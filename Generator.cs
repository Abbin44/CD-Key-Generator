using System;
using System.Linq;
using System.Threading;

namespace CD_Key
{
    class Program
    {
        static int keyLenght = 14; //lenght = keyLenght + 2
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            do
            {
                Console.WriteLine("Enter 1 for generating a key");
                Console.WriteLine("Enter 2 for checking a key");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        GenerateKey();
                        Console.Clear();
                        break;
                    case "2":
                        Console.Clear();
                        CheckKey();
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Write a valid key");
                        break;
                }
            }
            while (Console.ReadKey(true).Key == ConsoleKey.Escape);
            {
                Environment.Exit(0);
            }
        }

        static void GenerateKey()
        {
            string key = null;
            int[] intKey = new int[keyLenght];

            int sum = 0;
            for (int i = 0; i < keyLenght - 1; i++)
            {
                if (i == keyLenght - 2)
                {
                    for (int j = 0; j < intKey.Length; j++)
                    {
                        key += intKey[j];
                        sum = intKey.Sum(); //Get sum of all previous number
                    }
                    key += sum; //add sum to the end
                    key += RndNbr();
                }
                else
                    intKey[i] = Convert.ToInt32(RndNbr());

                //IT WILL BE BUGGY WITHOUT SLEEP
                Thread.Sleep(10);
            }

            //Print result
            Console.WriteLine("Valid key: " + key.ToString());
            Console.WriteLine("Press any key to go back to main menu.");
            Console.ReadLine();
            Console.Clear();
            Menu();
        }
        static void CheckKey()
        {
            int checkSum = 0; // Value of the 2nd and 3rd last numbers
            string key = null; //Entire inputted CD key
            string keyValue = null; // Value of all numbers exept the 2nd and 3rd last

            Console.WriteLine("Enter a CD Key: ");
            key = Console.ReadLine();

            //Create a char array with all numbers and convert it to an int array
            char[] keyArray = new char[key.Length];
            keyArray = key.ToCharArray();
            int[] entireKeyArray = keyArray.Select(i => Int32.Parse(i.ToString())).ToArray();

            if (keyArray.Length == keyLenght + 3)
            {
                //GET SUM OF ALL NUMBERS EXEPT LAST TWO
                keyValue = entireKeyArray[keyArray.Length - 3].ToString();
                keyValue += entireKeyArray[keyArray.Length - 2].ToString();
                int keySum = Convert.ToInt32(keyValue);

                entireKeyArray[keyArray.Length - 1] = 0;
                entireKeyArray[keyArray.Length - 2] = 0;
                entireKeyArray[keyArray.Length - 3] = 0;
                checkSum = entireKeyArray.Sum();

                PrintCheck(keySum, checkSum);
            }
            else
            {
                Console.WriteLine("Invalid Key!");
                Console.WriteLine("Press any key to go back to main menu.");
                Console.ReadLine();
                Console.Clear();
                Menu();
            }

        }

        static void PrintCheck(int keySum, int checkSum)
        {
            if (keySum == checkSum)
            {
                Console.WriteLine("Your key is valid!");
                Console.ReadKey();
                Console.Clear();
                Menu();
            }
            else
            {
                Console.WriteLine("Your key is definetly not valid 100%!");
                Console.ReadKey();
                Console.Clear();
                Menu();
            }
        }

        public static string RndNbr()
        {
            Random random = new Random();
            string[] numbers = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            //REWRITE TO USE ARRAY AND RANDOMIZE INDEX
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 1)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
