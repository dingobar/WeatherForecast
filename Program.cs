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

            var forecasts = wf.product.time.OrderBy(x => x.from).Where(x => x.ForecastLength <= 1).ToList();


            foreach (var forecast in forecasts)
            {
                string humanForecast;
                switch (forecast.ForecastType)
                {
                    case "general":
                        humanForecast = $"{forecast.from.ToString("o")} In {forecast.TimeUntil} the wind will blow {forecast.location.windSpeed.mps} ms from the {forecast.location.windDirection.name} and the temperature will be {forecast.location.temperature.value} {forecast.location.temperature.unit}"; break;
                    case "precipitation":
                        humanForecast = $"{forecast.from.ToString("o")} In {forecast.TimeUntil} it will rain {forecast.location.precipitation.value} {forecast.location.precipitation.unit} over 1 hour. "; break;
                    default:
                        humanForecast = $"ForecastType {forecast.ForecastType} is not known."; break;

                }
                Console.WriteLine(humanForecast);
            }

            Console.WriteLine("Finished");
        }
    }
}
