﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace LearningDiary
{
    class LearningDiaryUI
    {

        public LearningDiary ObjectStorage;

        public LearningDiaryUI(LearningDiary objectStorage)
        {
            this.ObjectStorage = objectStorage;
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
                    ConsoleTopicSelectionLogic();
                }

                else if (command == "3")
                {
                    try
                    {
                        List<string> topicParameters = AskTopicParameters();
                        this.ObjectStorage.AddTopicToDiary(topicParameters[0], topicParameters[1], Convert.ToDouble(topicParameters[2]), "web");
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine(e);
                    }

                }

                else if (command == "4")
                {
                    command = Console.ReadLine();
                }

            }
        }

        private void ConsoleTopicSelectionLogic()
        {
            Console.Clear();
            List<Topic> topicObjects = this.ObjectStorage.GetAllTopics();
            int menuRowCount = topicObjects.Count;
            int currentMenuItem = 1;
            bool updateLoop = true;
            Console.WriteLine("Usage Instructions:\nDownArrow: Cycle topic list, S: Start selected topic, F: Finish selected topic, A: Add task to selected topic, E:Exit to main menu\n");
            PrintHeading(topicObjects[0]);
            (int cursorInitialLeftPos, int cursorInitialTopPos) = Console.GetCursorPosition();
            //Interaktiivinen menurakenne topicien valintaan. Vain nuolialas tuettuna
            while (updateLoop)
            {
                //non blocking input
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.DownArrow:

                            if (currentMenuItem == menuRowCount)
                            {
                                currentMenuItem = 1;
                            }
                            else
                            {
                                currentMenuItem++;
                            }
                            break;

                        case ConsoleKey.S:
                            topicObjects[currentMenuItem - 1].StartLearning();
                            break;

                        case ConsoleKey.F:
                            topicObjects[currentMenuItem - 1].FinishLearning();
                            break;

                        case ConsoleKey.A:
                            List<string> topicParameters = AskTaskParameters();
                            this.ObjectStorage.AddTaskToTopic(topicObjects[currentMenuItem-1].id, topicParameters[0], topicParameters[1], topicParameters[2], DateTime.Parse(topicParameters[3]));
                            break;

                        case ConsoleKey.E:
                            updateLoop = false;
                            break;
                        default:
                            break;
                    }
                }

                topicObjects = this.ObjectStorage.GetAllTopics();
                if (topicObjects.Count > 0)
                {
                    DrawTopicTable(cursorInitialLeftPos,cursorInitialTopPos,currentMenuItem,topicObjects);
                    Thread.Sleep(50);
                }
                else
                {
                    Console.WriteLine("No Topics in Learning Diary");
                }
            }
        }


        private static void DrawTopicTable(int tableStartLeft, int tableStartTop, int selectedRow, List<Topic> topics)
        {
            //Kursori aina samaan paikkaan draw callin alussa
            Console.SetCursorPosition(tableStartLeft, tableStartTop);
            int rowsToDraw = topics.Count;
            int currentRow = 1;
            bool colorThisRow;
            foreach (Topic item in topics)
            {
                if (currentRow == selectedRow)
                {
                    colorThisRow = true;
                }
                else
                {
                    colorThisRow = false;
                }
                PrintLearningTopicRow(item, colorThisRow);
                Console.SetCursorPosition(tableStartLeft, tableStartTop + 1);
                if (rowsToDraw == currentRow)
                {
                    return;
                }
                currentRow++;
            }
        }

        private static void PrintLearningTopicRow(Topic item, bool colorRow)
        {
            if (colorRow == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            string str = "";
            foreach (var property in item.GetType().GetProperties())
            {
                string prop = property.GetValue(item).ToString();

                if (prop.IndexOf("Task") != -1)
                {
                    str += StringFormatterToTable(item.TasksRelatedToTopic.Count.ToString());
                }

                else if (property.Name.IndexOf("TimeSpent") != -1)
                {
                    double converted = Convert.ToDouble(prop);
                    str += StringFormatterToTable(Math.Round(converted).ToString() + "s");
                }

                else
                {
                    str += StringFormatterToTable(prop);
                }

            }
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.White;
        }


        private static List<string> AskTopicParameters()
        {
            List<string> parameters = new List<string>();

            Console.WriteLine("Input title");
            parameters.Add(Console.ReadLine());

            Console.WriteLine("Input description");
            parameters.Add(Console.ReadLine());

            Console.WriteLine("Input estimated time to master");
            parameters.Add(Console.ReadLine());

            return parameters;

        }

        private static List<string> AskTaskParameters()
        {
            List<string> parameters = new List<string>();

            Console.WriteLine("Input title");
            parameters.Add(Console.ReadLine());

            Console.WriteLine("Input description");
            parameters.Add(Console.ReadLine());

            Console.WriteLine("Input notes");
            parameters.Add(Console.ReadLine());

            Console.WriteLine("Input deadline (if empty default is tomorrow)");
            string input = Console.ReadLine();
            if (input == "")
            {
                input = DateTime.Now.AddDays(1).ToString();
            }
            parameters.Add(input);


            return parameters;

        }

        private static void PrintInstructions()
        {
            Console.WriteLine("\nUsage instructions:\n");
            Console.WriteLine("0 Exit program");
            Console.WriteLine("1 List all learning topics");
            Console.WriteLine("3 Add Topic");
        }

        private static void PrintHeading(LearningDiaryItem item)
        {
            const string UNDERLINE = "\x1B[4m";
            const string RESET = "\x1B[0m";
            string str = UNDERLINE;
            foreach (var property in item.GetType().GetProperties())
            {

                str += StringFormatterToTable(property.Name);
            }
            str += RESET;
            Console.WriteLine(str);
            
        }

        private static void PrintAllTasksRelatedToTopic(List<Task> items)
        {
            string str = "";

            foreach (var item in items)
            {
                foreach (var property in item.GetType().GetProperties())
                {
                    string prop = property.GetValue(item).ToString();
                    str += StringFormatterToTable(prop);
                }
            }
            Console.WriteLine(str);
        }

        private static string StringFormatterToTable(string strToFormat)
        {
            return String.Format("|{0,-21}|", strToFormat);
        }

    }
}