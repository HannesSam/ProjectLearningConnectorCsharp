using ProjectLearningConnectorCsharp.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectLearningConnectorCsharp
{
    class Api
    {
        private const string BasePath = "https://localhost:44302";
        private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri(BasePath) };
        private readonly string ApiKey;
        private readonly string ProgTaskId;

        public Api(string apiKey, string assignmentId)
        {
            ApiKey = apiKey;
            ProgTaskId = assignmentId;
        }

        public async Task<string> GetAssignment()
        {
            var payload = new { ProgTaskId, ApiKey };

            var stringPayload = JsonSerializer.Serialize(payload);

            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var httpResponse = await _client.PostAsync("/api/ProgTask/Start", httpContent);

            string responseContent = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Exception:" + httpResponse);
                Console.WriteLine();
                Console.WriteLine("Fatal Error: could not start a new game");
                Environment.Exit(1);
            }


            return responseContent;
        }

        public async Task<string> SubmitAssignment(dynamic returnValues)
        {
            var payload = new { ProgTaskId, ApiKey, Result = returnValues };

            var stringPayload = JsonSerializer.Serialize(payload);

            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var httpResponse = await _client.PostAsync("/api/ProgTask/Submit", httpContent);

            string responseContent = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Exception:" + httpResponse);
                Console.WriteLine();
                Console.WriteLine("Fatal Error: could not start a new game");
                Environment.Exit(1);
            }
            return responseContent;
        }
    }
}
