using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;


namespace eleventh
{

    class MetAPIRequester
    {
        private HttpClient client = new HttpClient();
        private double lat = 55.706477;
        private double lon = 12.562920;
        private double msl = 10;
        private string metURL = "https://api.met.no/weatherapi/locationforecast/1.9/.json";

        public async Task<WeatherForecast> GetForecastAsync()
        {
            var parametersToAdd = new System.Collections.Generic.Dictionary<string, string> {
                { "lat", lat.ToString() },
                { "lon", lon.ToString() },
                { "msl", msl.ToString() }
                };
            var requestUri = QueryHelpers.AddQueryString(metURL, parametersToAdd);
            Console.WriteLine(requestUri.ToString());
            var result = await client.GetAsync(requestUri);
            Console.WriteLine(result.StatusCode);
            var resultString = await result.Content.ReadAsStringAsync();

            var weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(resultString);

            return weatherForecast;
        }
    }
}