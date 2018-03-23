using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            byte secretNumber = 0;
            byte playerGuess = 0;
            byte triesCount = 0;
            bool retry = false;
            bool found = false;
            bool lost = false;

            InitConsole();

            do
            {
                secretNumber = (byte)rnd.Next(10, 100);
                Reset(ref triesCount, ref found, ref lost);

                do
                {
                    triesCount++;
                    playerGuess = GetUserGuess();
                    found = CheckGuess(playerGuess, secretNumber);
                    lost = CheckTries(triesCount);

                    if (found)
                    {
                        Console.WriteLine($"YOU FOUND THE SECRET NUMBER ({secretNumber}) IN ({triesCount}) {(triesCount == 1 ? "try" : "tries")}\n");
                    }
                    else if (lost)
                    {
                        Console.WriteLine($"MAX TRIES ({triesCount}) REACHED, THE SECRET NUMBER WAS {secretNumber}\n");
                    }

                    if (found || lost)
                    {
                        retry = GetUserChoice();
                    }
                } while (!found && !lost);
            } while (retry);

            Console.WriteLine("Press Any Key to Exit.");
            Console.ReadLine();
        }

        static void InitConsole()
        {
            Console.Title = "GUESS THE NUMBER";
            Console.Clear();
            Console.BufferWidth = 100;
            Console.BufferHeight = 30;
            Console.WindowWidth = 100;
            Console.WindowHeight = 30;
        }

        static void Reset(ref byte triesCount, ref bool found, ref bool lost)
        {
            triesCount = 0;
            found = false;
            lost = false;
            Console.Clear();
            Console.WriteLine("**********************************");
            Console.WriteLine("***** FIND THE SECRET NUMBER *****");
            Console.WriteLine("**********************************");
        }

        static byte GetUserGuess()
        {
            bool isInvalidInput = true;
            byte guess = 0;

            do
            {
                Console.Write("Enter a 2 digits number, between 10 and 99: \t");

                string userInput = Console.ReadLine();

                if (byte.TryParse(userInput, out guess))
                {
                    isInvalidInput = false;
                }
                else
                {
                    Console.WriteLine("Invalid number");
                }
            } while (isInvalidInput);

            return guess;
        }

        static bool GetUserChoice()
        {
            bool isInvalidChoice = true;
            bool retry = false;
            do
            {
                Console.WriteLine("Press [0] to Quit, [1] to Retry: \t");
                string userInput = Console.ReadLine();
                if (userInput == "1" || userInput == "0")
                {
                    isInvalidChoice = false;
                    retry = userInput == "1" ? true : false;
                }
                else
                {
                    Console.WriteLine("Invalid Choice...");
                }
            } while (isInvalidChoice);

            return retry;
        }

        static bool CheckGuess(byte guess, byte secret)
        {
            if (secret > guess)
            {
                Console.WriteLine($"The secret number is greater than {guess}");
            }
            else if (secret < guess)
            {
                Console.WriteLine($"The secret number is lower than {guess}");
            }

            return guess == secret;
        }

        static bool CheckTries(byte tries)
        {
            return tries >= byte.MaxValue;
        }
    }
}
