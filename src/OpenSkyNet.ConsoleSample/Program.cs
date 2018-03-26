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

                Console.WriteLine(result.TimeStamp);

                foreach (var item in result.States)
                    Console.WriteLine(item.OriginCountry);

                var x = client.TrackFlight("acb84d");
                x.Subscribe(new FlightSubscription());

                await Task.Delay(120 * 1000);
            }
        }
    }

    class FlightSubscription : IObserver<IStateVector>
    {
        public void OnCompleted()
        {
            Console.WriteLine("completed");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine(error.Message);
        }

        public void OnNext(IStateVector value)
        {
            Console.WriteLine(value.CallSign + " - " + value.Latitude + " | " + value.Longitude);
        }
    }
}
