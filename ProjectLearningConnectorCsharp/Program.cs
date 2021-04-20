using System;

namespace ProjectLearningConnectorCsharp
{
    class Program
    {

        //Enter your ApiKey
        private const string ApiKey = "9159";
        //Pick your assignment
        private const string Assignment = "601";

        private static readonly GameLayer GameLayer = new GameLayer(ApiKey, Assignment);
        static void Main(string[] args)
        {
            var result = GameLayer.StartAssignment();

            Console.WriteLine(result);
            Console.ReadLine();
        }

       public static int[] Test(int[] arrayOne, int[] arrayTwo)
        {
            int[] test = new int[arrayOne.Length];
            return test;
        }
    }
}
