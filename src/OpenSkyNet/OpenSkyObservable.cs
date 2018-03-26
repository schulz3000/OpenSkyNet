using System;
using System.Collections.Generic;

namespace OpenSkyNet
{
    class OpenSkyObservable : IObservable<IStateVector>
    {
        readonly List<IObserver<IStateVector>> observers = new List<IObserver<IStateVector>>();

        public string Icao24 { get; }

        public int Count => observers.Count;

        public OpenSkyObservable(string icao24)
        {
            Icao24 = icao24;
        }

        public IDisposable Subscribe(IObserver<IStateVector> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Unsubscriber(observers, observer);
        }

        class Unsubscriber : IDisposable
        {
            readonly List<IObserver<IStateVector>> _observers;
            readonly IObserver<IStateVector> _observer;

            public Unsubscriber(List<IObserver<IStateVector>> observers, IObserver<IStateVector> observer)
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

        public void TrackVector(IStateVector vector)
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
