using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLearningConnectorCsharp
{
    class API
    {
        private const string BasePath = "LocalHost";
        private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri(BasePath) };

        public API(string apiKey)
        {
            _client.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }

        public async Task<State> NewGame(string map)
        {
            var data = new StringContent(map, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("new", data);

            var result = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Exception:" + result);
                Console.WriteLine();
                Console.WriteLine("Fatal Error: could not start a new game");
                Environment.Exit(1);
            }

            return JsonConvert.DeserializeObject<State>(result);
        }
    }
}
