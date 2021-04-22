using ProjectLearningConnectorCsharp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ProjectLearningConnectorCsharp
{
    class GameLayer
    {
        private readonly Api api;
        private readonly AssignmentManager assignmentManager;
        public GameLayer(string APIKey, string assignmentId)
        {
            api = new Api(APIKey, assignmentId);
            assignmentManager = new AssignmentManager(assignmentId);
        }

        public dynamic StartAssignment()
        {
            string progTaskString = api.GetAssignment().Result;
            IncomingProgTask progTask = JsonSerializer.Deserialize<IncomingProgTask>(progTaskString);
            var returnValues = AssignmentManager.RunAssignment(progTask.IncomingAssignment.ToString());

            var resultOnTests = SubmitAssignment(returnValues);

            return resultOnTests;
        }

        public dynamic SubmitAssignment(dynamic returnValues)
        {
            var result = api.SubmitAssignment(returnValues).Result;
            return result;
        }
    }
}
