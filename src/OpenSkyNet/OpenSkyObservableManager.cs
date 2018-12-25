using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSkyNet
{
    class OpenSkyObservableManager
    {
        readonly HashSet<OpenSkyObservable> observers;
        readonly OpenSkyClient service;

        public OpenSkyObservableManager(OpenSkyClient api, CancellationToken token)
        {
            service = api;
            observers = new HashSet<OpenSkyObservable>();
            token.Register(CompleteObservers);
            Task.Run(() => StartTimer(token));
        }

        async Task StartTimer(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await OnTick(cancellationToken).ConfigureAwait(false);
                await Task.Delay(10000, cancellationToken).ConfigureAwait(false);
            }
        }

        async Task OnTick(CancellationToken cancellationToken)
        {
            if (observers.Count == 0)
                return;

            var icao24s = observers.Where(w => w.Count > 0).Select(s => s.Icao24).ToArray();

            if (icao24s.Length == 0)
                return;

            var result = await service.GetStatesAsync(icao24: icao24s, token: cancellationToken).ConfigureAwait(false);

            if (result.States == null)
            {
                foreach (var item in observers)
                    item.VectorNotAvailable();

                return;
            }

            foreach (var item in result.States)
            {
                var observer = observers.FirstOrDefault(f => f.Icao24 == item.Icao24);
                observer?.TrackVector(item);
            }

            var notavailable = result.States.Select(s => s.Icao24).Except(icao24s);
            foreach (var item in notavailable)
            {
                var observer = observers.FirstOrDefault(f => f.Icao24 == item);
                observer?.VectorNotAvailable();
            }
        }

        public IObservable<IOpenSkyStateVector> GetObservableFor(string icao24)
        {
            var observer = observers.FirstOrDefault(f => f.Icao24 == icao24);

            if (observer == null)
            {
                observer = new OpenSkyObservable(icao24);
                observers.Add(observer);
            }

            return observer;
        }

        void CompleteObservers()
        {
            foreach (var item in observers)
                item.EndTracking();
        }
    }
}
