using CoronavirusBrasil.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoronavirusBrasil
{
    public class CoronavirusService
    {
        private static readonly string API_BASE = "https://covid-193.p.rapidapi.com/statistics";
        private readonly string API_KEY;

        public CoronavirusService(string apiKey)
        {
            API_KEY = apiKey;
        }

        public async Task<Response> GetBrazilStats()
        {
            var response = await GetResponseAsync();
            var statsList = JsonConvert.DeserializeObject<ApiResult>(response);
            return statsList.Response.FirstOrDefault(x => x.Country == "Brazil");
        }

        private async Task<string> GetResponseAsync()
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "covid-193.p.rapidapi.com");
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", API_KEY);
            using var response = await httpClient.GetAsync(API_BASE);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
