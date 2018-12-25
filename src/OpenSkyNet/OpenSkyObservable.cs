using System;
using System.Collections.Generic;

namespace OpenSkyNet
{
    class OpenSkyObservable : IObservable<IOpenSkyStateVector>
    {
        readonly List<IObserver<IOpenSkyStateVector>> observers = new List<IObserver<IOpenSkyStateVector>>();

        public string Icao24 { get; }

        public int Count => observers.Count;

        public OpenSkyObservable(string icao24)
        {
            Icao24 = icao24;
        }

        public IDisposable Subscribe(IObserver<IOpenSkyStateVector> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Unsubscriber(observers, observer);
        }

        class Unsubscriber : IDisposable
        {
            readonly List<IObserver<IOpenSkyStateVector>> _observers;
            readonly IObserver<IOpenSkyStateVector> _observer;

            public Unsubscriber(List<IObserver<IOpenSkyStateVector>> observers, IObserver<IOpenSkyStateVector> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public void TrackVector(IOpenSkyStateVector vector)
        {
            foreach (var item in observers)
                item.OnNext(vector);
        }

        public void EndTracking()
        {
            foreach (var item in observers)
                item.OnCompleted();

            observers.Clear();
        }

        public void VectorNotAvailable()
        {
            foreach (var item in observers)
                item.OnError(new OpenSkyNetException("Icao24 not available"));
        }
    }
}
