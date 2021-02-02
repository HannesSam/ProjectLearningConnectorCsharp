using System;

namespace ProjectLearningConnectorCsharp
{
    class Program
    {

        //Enter your ApiKey
        private const string ApiKey = "5423";
        //Pick your assignment
        private const string Assignment = "01-02-01";

        private static readonly GameLayer GameLayer = new GameLayer(ApiKey, Assignment);
        static void Main(string[] args)
        {
            var result = GameLayer.StartAssignment();

            Console.WriteLine(result);
            Console.ReadLine();
        }

       public static int Test(int numberOne, int numberTwo)
        {
            return numberOne * numberTwo;
        }
    }
}
