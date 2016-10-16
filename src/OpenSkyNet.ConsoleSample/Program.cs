using System;
using System.Threading.Tasks;

namespace OpenSkyNet.ConsoleSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();

            Console.Read();
        }

        static Task MainAsync(string[] args)
        {
            return Exec();
        }

        async static Task Exec()
        {
            using (var client = new OpenSkyApi())
            {
                var result = await client.GetStatesAsync(icao24: new[] { "abc9f3", "3e1bf9" });

                Console.WriteLine(result.TimeStamp);

                foreach (var item in result.States)
                    Console.WriteLine(item.OriginCountry);
            }
        }
    }
}
