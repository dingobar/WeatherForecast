using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eleventh
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new MetAPIRequester();

            var wf = await client.GetForecastAsync();

            var forecasts = wf.product.time;

            var precipitationforecasts = forecasts.OrderBy(x => x.from).Where(x => x.location.precipitation != null && x.ForecastLength == 1).ToList();

            foreach(var forecast in precipitationforecasts){
                Console.WriteLine($"");
            }

            Console.WriteLine("Finished");
        }
    }
}
