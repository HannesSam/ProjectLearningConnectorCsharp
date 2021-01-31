using System;

namespace ProjectLearningConnectorCsharp
{
    class Program
    {

        //Enter your ApiKey
        private const string ApiKey = "5423";
        //Pick your assignment
        private const string Assignment = "01-01-01";

        private static readonly GameLayer GameLayer = new GameLayer(ApiKey);
        static void Main(string[] args)
        {
            var test = GameLayer.StartAssignment("01-01-01");
            Console.WriteLine("End");
            Console.ReadLine();


        }

       public static long Test(long numberOne, long numberTwo)
        {
            return numberOne + numberTwo;
        }
    }
}
