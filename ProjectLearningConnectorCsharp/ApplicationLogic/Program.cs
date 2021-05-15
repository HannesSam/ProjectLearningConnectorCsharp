using System;

namespace ProjectLearningConnectorCsharp
{
    class Program
    {
        static void Main()
        {
            GameLayer GameLayer = new(WriteCodeHere.ApiKey, WriteCodeHere.TaskID);
            var result = GameLayer.StartAssignment();

            Console.WriteLine(result);
            Console.ReadLine();
        }

    }
}
