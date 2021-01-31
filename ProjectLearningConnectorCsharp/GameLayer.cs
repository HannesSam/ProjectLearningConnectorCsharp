using ProjectLearningConnectorCsharp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectLearningConnectorCsharp
{
    class GameLayer
    {
        private readonly Api _api;
        public GameLayer(string APIKey)
        {
            _api = new Api(APIKey);
        }

        public dynamic StartAssignment(string assignmentId)
        {
            var test = _api.GetAssignmentPost(assignmentId).Result;
            var returnValues = AssignmentManager.RunAssignment(test, assignmentId);

            var resultOnTests = SubmitAssignment(returnValues, assignmentId);

            return resultOnTests;
        }

        public dynamic SubmitAssignment(dynamic returnValues, string assignmentId)
        {
            var result = _api.SubmitAssignmentPost(returnValues, assignmentId);
            return null;
        }
    }
}
