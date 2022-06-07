using System;

namespace LearningDiary
{
    class LearningDiaryUI
    {
        

        public LearningDiaryUI()
        {
        }

        public void Execute()
        {
            while (true)
            {
                PrintInstructions();
                Console.Write("Input command: ");
                string command = Console.ReadLine();

                if (command == "0")
                {

                    Console.WriteLine("Exiting");
                    break;
                }
                else if (command == "1")
                {
                    Console.Write("Input product name: ");
                }

                else if (command == "2")
                {
                    
                }

                else if (command == "3")
                {
                    
                }

                else if (command == "4")
                {
                    
                }

            }
        }
        private static void PrintInstructions()
        {
            Console.WriteLine("\nUsage instructions:\n");
            Console.WriteLine("0 Exit program");
            Console.WriteLine("1 Add new product to shopping list");
            Console.WriteLine("2 Print the most expensive product in shopping list");
            Console.WriteLine("3 Print the cheapest product in shopping list");
            Console.WriteLine("4 Print shopping list");
        }

    }
}