using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicAlgorithmApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****************-TASK 1-****************");
            var requestNumbers = new List<int>() { 1, 2, 1, 3 };
            
            if (requestNumbers.Any())
            {
                var resultNumbers = FindUniqueNumbers(requestNumbers);
                if (resultNumbers.Any())
                {
                    if (resultNumbers.Count() == 1)
                    {
                        Console.WriteLine("Result number is: " + resultNumbers[0]);
                    }
                    else
                    {
                        Console.WriteLine("Result numbers are: " + string.Join(", ", resultNumbers));
                    }
                }
                else
                {
                    Console.WriteLine("There is no number that occur exactly once");
                }
            }
            else
            {
                Console.WriteLine("Please insert valid numbers.");
            }


            Console.WriteLine("****************-TASK 2-****************");
            var requestNumbers2 = new List<int>() { 5, 9, 7, 11 };

            if (requestNumbers2.Any() && requestNumbers2.Count() >= 2)
            {
                var resultNumbers2 = FindmaxSum(requestNumbers2);
                Console.WriteLine("Result number is: " + resultNumbers2);
            }
            else
            {
                Console.WriteLine("Please insert valid numbers.");
            }
        }

        private static List<int> FindUniqueNumbers(List<int> requestNumbers)
        {
            var numbersCount = new Dictionary<int, int>();
            var resultNumbers = new List<int>();

            foreach (var number in requestNumbers)
            {
                if (numbersCount.ContainsKey(number))
                {
                    numbersCount[number]++;
                }
                else
                {
                    numbersCount[number] = 1;
                }
            }

            foreach (var number in numbersCount)
            {
                if (number.Value == 1)
                {
                    resultNumbers.Add(number.Key);
                }
            }

            return resultNumbers;

        }

        private static List<int> FindUniqueNumbersLinq(List<int> requestNumbers)
        {
            // best implementation using Linq
            return requestNumbers.GroupBy(n => n).Where(o => o.Count() == 1).Select(p => p.Key).ToList();
        }

        private static int FindmaxSum(List<int> requestNumbers)
        {
            var number1 = 0;
            var number2 = 0;

            foreach (var number in requestNumbers)
            {
                if (number > number1)
                {
                    number2 = number1;
                    number1 = number;
                }
                else if (number > number2)
                {
                    number2 = number;
                }
            }

            return number1 + number2;
        }

        private static int FindmaxSumLinq(List<int> requestNumbers)
        {
            // best implementation using Linq
            var sortedNumbers = requestNumbers.OrderByDescending(n => n).ToList();
            return sortedNumbers[0] + sortedNumbers[1];
        }
    }
}
