using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLearningConnectorCsharp.Models.AssignmentType
{
    class ReturnLayer
    {
        public string APIKey { get; set; }
        public string AssignmentId { get; set; }
        public dynamic Result { get; set; }
    }
}
