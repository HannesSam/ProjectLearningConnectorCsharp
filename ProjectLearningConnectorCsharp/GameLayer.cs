using ProjectLearningConnectorCsharp.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
            var assignment = api.GetAssignmentPost().Result;
            var returnValues = assignmentManager.RunAssignment(assignment);

            var resultOnTests = SubmitAssignment(returnValues);

            return resultOnTests;
        }

        public dynamic SubmitAssignment(dynamic returnValues)
        {
            var result = api.SubmitAssignmentPost(returnValues).Result;
            return result;
        }
    }
}
