using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLearningConnectorCsharp
{
    class SolutionArea
    {
        //Enter your ApiKey
        public static string ApiKey = "9159";
        //Pick your assignment
        public static string TaskID = "101";

        //This is the starting point of your solution
        public static int Start(int firstNummber, int secondNumber)
        {
            return firstNummber + secondNumber;
        }
    }
}
