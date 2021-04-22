using System;

namespace ProjectLearningConnectorCsharp
{
    class Program
    {
        static void Main()
        {
            GameLayer GameLayer = new(SolutionArea.ApiKey, SolutionArea.TaskID);
            var result = GameLayer.StartAssignment();

            Console.WriteLine(result);
            Console.ReadLine();
        }

    }
}
