using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectLearningConnectorCsharp.Models
{
    public class IncomingProgTask
    {
        [JsonPropertyName("progTaskID")]
        public string ProgTaskID { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("outgoingAssignment")]
        public object IncomingAssignment { get; set; }
    }
}
