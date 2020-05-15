using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SlotMachine
{
    public class SlotMachineRepo
    {
        readonly Random rand = new Random();
        readonly string[] wheelOne = { "cherry ", "Diamond", "  Bar  ", "   $   ", };
        public string SpinTheWheel()
        {
            int sleepTime = rand.Next(10);
            Thread.Sleep(sleepTime);
            int wheelIndex = rand.Next(wheelOne.Length);
            return wheelOne[wheelIndex];
            
        }

        public int WinningsCalc(string wheel, double currentBet)
        {
            switch (wheel.ToLower())
            {
                case "diamond":
                    //bet x 3 or ^3
                    return (int)Math.Pow(currentBet * 2, 3);
                case "  cherry  ":
                    return (int)Math.Pow(currentBet * 2, 2);
                //bet x 2 or ^2
                case "   $   ":
                    //get your money back
                    return (int)currentBet * 10;
                case "  bar  ":
                    return 0;
                default:
                    //too bad
                    return 0;
            }
        }
        public ConsoleColor GetConsoleColor(string wheel)
        {
            switch (wheel.ToLower())
            {
                case " cherry ":
                    return ConsoleColor.Red;
                case "diamond":
                    return ConsoleColor.Blue;
                case "  bar  ":
                    return ConsoleColor.Yellow;
                case "   $   ":
                    return ConsoleColor.Green;
                default:
                    return ConsoleColor.White;
            }
        }

        public double BuyToken(double moneyWallet)
        {
            double tokenRequest = 0;
            bool tokenRequestIsValid = true;
            while (tokenRequestIsValid)
            {
                Console.Clear();
                Console.WriteLine("You need more tokens! \n" +
                   "We'll happily sell you more - they are $10 each. \n" +
                   "How many would you like to buy?");
                string input = Console.ReadLine();
                bool b = double.TryParse(input, out tokenRequest);
                int maxToken = (int)Math.Floor(moneyWallet / 10);

                if (b && tokenRequest * 10 > moneyWallet)
                {
                    Console.WriteLine($"Input a number less than {maxToken}.");
                    Thread.Sleep(2000);
                }
                else if (b && tokenRequest * 10 <= moneyWallet)
                {
                    tokenRequestIsValid = false;
                }
                else
                {
                    Console.WriteLine($"Please enter a number less than {maxToken}");
                    Thread.Sleep(2000);
                }
            }
            return tokenRequest;
        }
    }
}
