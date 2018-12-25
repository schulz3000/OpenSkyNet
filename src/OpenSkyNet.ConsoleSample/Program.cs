using System;
using System.Threading.Tasks;

namespace OpenSkyNet.ConsoleSample
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Console.ReadKey();

            await Exec();

            Console.Read();
        }

        async static Task Exec()
        {
            using (var client = new OpenSkyClient())
            {
                var result = await client.GetStatesAsync();

                Console.WriteLine(result.Time);

                //foreach (var item in result.States)
                //    Console.WriteLine(item.OriginCountry);

                var aircraftTrack = await client.GetTrackByAircraftAsync("acb84d");
                Console.WriteLine(aircraftTrack.CalllSign);

                var flights = await client.GetFlights(DateTime.Now.AddMinutes(-110), DateTime.Now);
                Console.WriteLine(flights[0].CallSign);

                var x = client.TrackFlight("acb84d");
                x.Subscribe(new FlightSubscription());

                await Task.Delay(120 * 1000);
            }
        }
    }

    class FlightSubscription : IObserver<IOpenSkyStateVector>
    {
        public void OnCompleted()
        {
            Console.WriteLine("completed");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine(error.Message);
        }

        public void OnNext(IOpenSkyStateVector value)
        {
            Console.WriteLine(value.CallSign + " - " + value.Latitude + " | " + value.Longitude);
        }
    }
}
