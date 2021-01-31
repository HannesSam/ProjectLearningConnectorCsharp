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
        private const string BasePath = "https://localhost:44374";
        private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri(BasePath) };
        private readonly string APIKey;

        public Api(string apiKey)
        {
            APIKey = apiKey;
        }

        public async Task<string> GetAssignmentPost(string assignmentId)
        {
            var payload = new { AssignmentId = assignmentId, APIKey = APIKey };

            var stringPayload = JsonSerializer.Serialize(payload);

            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var httpResponse = await _client.PostAsync("/api/Assignment/Start", httpContent);

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

        public async Task<string> SubmitAssignmentPost(dynamic returnValues, string assignmentId)
        {
            var payload = new { AssignmentId = assignmentId, APIKey = APIKey, Result = returnValues };

            var stringPayload = JsonSerializer.Serialize(payload);

            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var httpResponse = await _client.PostAsync("/api/Assignment/Submit", httpContent);

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
