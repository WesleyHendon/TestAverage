using System;
using System.Collections.Generic;

namespace TestAverage
{
    class Program
    {
        public static int inputCounter;
        public static bool recievingInput;
        public static List<int> allTestScores = new List<int>();

        static void Main(string[] args)
        {
            recievingInput = true;
            inputCounter = 1;
            allTestScores.Clear();
            while (recievingInput)
            {
                int newScore = PromptUserForTestScore();
                if (newScore >= 0)
                {
                    allTestScores.Add(newScore);
                    inputCounter++;
                }
                else if (newScore == -2)
                {
                    Console.WriteLine("Remove test score number: ");
                    int toRemove = -1;
                    try
                    {
                        toRemove = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine("Failed to remove test score");
                    }
                    if (toRemove >= 1 && !(toRemove > allTestScores.Count))
                    {
                        List<int> newList = new List<int>();
                        int counter = 0;
                        int removedScore = 0;
                        foreach (int val in allTestScores)
                        {
                            if (counter != (toRemove-1))
                            {
                                newList.Add(val);
                            }
                            else
                            {
                                removedScore = val;
                            }
                            counter++;
                        }
                        allTestScores = newList;
                        Console.WriteLine("The test score of " + removedScore + "will not be included in the average!");
                    }
                }
                Console.WriteLine("\n");
            }
            if (allTestScores.Count >= 0)
            {
                int testScoreSum = 0;
                foreach (int score in allTestScores)
                {
                    testScoreSum += score;
                }
                int testScoreAverage = testScoreSum / (allTestScores.Count >= 1 ? allTestScores.Count : 1);
                Console.WriteLine($"Your test average is {testScoreAverage}");
            }
        }

        public static int PromptUserForTestScore()
        {
            string stringModifier = (inputCounter == 1) ? "first" : (inputCounter == 2) ? "second" : (inputCounter == 3) ? "third" : "next";
            Console.WriteLine("What is your " + stringModifier + " test score? (Press 'esc' to calculate the average, or 'spacebar' to view all entered scores");
            while(!(Console.KeyAvailable && (Console.ReadKey(true).Key == ConsoleKey.Escape || Console.ReadKey(true).Key == ConsoleKey.Spacebar)))
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    recievingInput = false;
                    return -1; // if the number is below 0, its not included in avg
                }
                if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                {
                    string allAveragesString = "";
                    int counter = 0;
                    foreach (int score in allTestScores)
                    {
                        allAveragesString += "Test Score " + (counter + 1) + ": " + score + "\n";
                        counter++;
                    }
                    Console.WriteLine("\nCurrent test scores included in avg:\n" + allAveragesString + "\n");
                    return -2;
                }
                try
                {
                    return Convert.ToInt32(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("\nTry that input again...\n");
                    return Convert.ToInt32(Console.ReadLine());
                }
            }
            return -1;
        }
    }
}
