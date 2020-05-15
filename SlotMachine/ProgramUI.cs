using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SlotMachine
{
    public class ProgramUI
    {
        //cond statement(s) that compare a wheel to output values 
        //loop to replay the game - DONE
        //conditional to prevent negative tokens - DONE
        //conditional to prevent negative money
        //build logic to keep improper entries from breaking code - DONE

        //private readonly RealConsole _console = new RealConsole();
        readonly SlotMachineRepo _slotMachineRepo = new SlotMachineRepo();

        double moneyWallet = 100;
        int tokenCount = 0;

        public void Run()
        {

            Console.WriteLine("Welcome to Simple Slots! Press any key to continue.");
            Console.ReadKey();

            bool wantToPlay = true;

            while (wantToPlay)
            {
                Console.Clear();
                Console.WriteLine($"Tokens remaining: {tokenCount} | Current cash: ${moneyWallet}");
                Thread.Sleep(400);
                if (tokenCount < 1)
                {
                    int tokenBought = (int)_slotMachineRepo.BuyToken(moneyWallet);
                    tokenCount += tokenBought;
                    moneyWallet -= tokenBought * 10;
                    Console.WriteLine($"Your new balance is {tokenCount} token(s) and ${moneyWallet}.\n" +
                        "Press any key to continue.");
                    Console.ReadKey();
                }

                double currentBet = 0;
                bool betIsVaild = false;
                while (!betIsVaild)
                {
                    Console.Clear();
                    Console.WriteLine("Please place a bet between 1 and 3 tokens.");
                    string stringBet = Console.ReadLine();
                    bool b = double.TryParse(stringBet, out currentBet);

                    if (b && currentBet <= tokenCount && currentBet <= 3)
                    {
                        tokenCount -= (int)currentBet;
                        betIsVaild = true;
                    }
                    else if (b && currentBet > 3)
                    {
                        Console.WriteLine("Please enter a bet that is 3 or fewer tokens.");
                        Console.ReadKey();
                    }
                    else if (b && tokenCount < currentBet)
                    {
                        Console.WriteLine($"You don't have enough tokens. Please enter a bet that is less than {tokenCount}");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"Please enter a valid bet less than {tokenCount}");
                        Console.ReadKey();
                    }
                }
                Console.Clear();
                Console.WriteLine($"Tokens remaining: {tokenCount} | Current cash: ${moneyWallet}");
                Console.WriteLine($"Thank you. Your current bet is {currentBet} token(s). Press any key to pull the lever.");
                Console.ReadKey();


                int winnings = 0;
                string wheelOne = _slotMachineRepo.SpinTheWheel();
                string wheelTwo = _slotMachineRepo.SpinTheWheel();
                string wheelThree = _slotMachineRepo.SpinTheWheel();

                //Attempt at color coordinating results from "SpinTheWheel" Methods
                //ConsoleColor consoleColorWheelOne = _slotMachineRepo.GetConsoleColor(wheelOne);
                //ConsoleColor consoleColorWheelTwo = _slotMachineRepo.GetConsoleColor(wheelTwo);
                //ConsoleColor consoleColorWheelThree = _slotMachineRepo.GetConsoleColor(wheelThree);

                if (wheelOne == wheelTwo && wheelOne == wheelThree)
                {
                    winnings += _slotMachineRepo.WinningsCalc(wheelOne, currentBet);
                }
                else if (wheelOne==wheelTwo || wheelOne==wheelThree )
                {
                    winnings += _slotMachineRepo.DoubleWinningsCalc(wheelOne, currentBet);
                }
                else if (wheelTwo == wheelThree)
                {
                    winnings += _slotMachineRepo.DoubleWinningsCalc(wheelTwo, currentBet);
                }

                moneyWallet += winnings;
                //if wheelOne==wheelTwo && wheelOne==WheelThree
                if (winnings == 0)
                {
                    Console.WriteLine($"\n\n ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░" +
                        $"\n ░░░{wheelOne}░░░{wheelTwo}░░░{wheelThree}░░░\n" +
                        $" ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░" +
                        $"\n\n\nYou didn't win anything. Better luck next time!");
                }
                else
                {
                    Console.WriteLine("\n\n ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░" +
                        $"\n ░░░{wheelOne}░░░{wheelTwo}░░░{wheelThree}░░░" +
                        "\n ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░" +
                        $"\n\n\nCongratulations, you won ${winnings}!");
                }


                Console.WriteLine($"Your new total is ${moneyWallet}.\n" +
                    $"Type Q then enter to end the game, or simply press enter to continue.");
                string continueInput = Console.ReadLine();
                if (continueInput.ToLower() == "q")
                {
                    Console.WriteLine($"Here are your final results! \n" +
                        $"Total Winnings: ${moneyWallet}.\n" +
                        $"GoodBye!!");
                    Thread.Sleep(5000);
                    wantToPlay = false;
                }
                else
                {
                    wantToPlay = true;
                }

                if (tokenCount <= 0 && moneyWallet <= 9)
                {
                    Console.Clear();
                    Console.WriteLine("You have no more tokens and your wallet is empty!\n" +
                        "Tough day at the casino...");
                    Thread.Sleep(5000);
                    wantToPlay = false;
                }
            }
        }
    }
}