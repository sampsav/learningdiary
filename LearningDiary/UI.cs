using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace LearningDiary
{
    class LearningDiaryUI
    {

        public LearningDiary ObjectStorage;
        public string searhcstr;
        public LearningDiaryUI(LearningDiary objectStorage)
        {
            this.ObjectStorage = objectStorage;
            this.searhcstr = "";
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

            }
        }

        private void ConsoleTopicSelectionLogic()
        {
            Console.Clear();
            List<Topic> topicObjects = this.ObjectStorage.GetAllTopics();

            if (topicObjects.Count == 0)
            {
                Console.WriteLine("No Topics in Learning Diary");
                Thread.Sleep(5000);
                return;
            }

            int menuRowCount = topicObjects.Count;
            int currentMenuItem = 1;
            bool updateLoop = true;
            Console.WriteLine("Usage Instructions:\nArrows: Cycle topic list, S: Start selected topic, F: Finish selected topic, A: Add task to selected topic, E:Exit to main menu, Q: activate topic search mode\n");
            PrintHeading(topicObjects[0]);
            Console.WriteLine("");
            (int cursorInitialLeftPos, int cursorInitialTopPos) = Console.GetCursorPosition();
            bool searchModeActive = false;
            while (updateLoop)
            {
                //non blocking input
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:

                            currentMenuItem--;
                            if(currentMenuItem < 1)
                            {
                                currentMenuItem = menuRowCount;
                                //tyhjennä key buffer
                                while (Console.KeyAvailable)
                                    Console.ReadKey(false);
                            }
                            break;

                        case ConsoleKey.DownArrow:

                            currentMenuItem++;
                            if (currentMenuItem > menuRowCount)
                            {

                                currentMenuItem = 1;
                                while (Console.KeyAvailable)
                                    Console.ReadKey(false);
                            }
                            break;

                        case ConsoleKey.S:
                            topicObjects[currentMenuItem - 1].StartLearning();
                            break;

                        case ConsoleKey.F:
                            topicObjects[currentMenuItem - 1].FinishLearning();
                            break;
                        case ConsoleKey.Q:
                            Console.Clear();
                            PrintHeading(topicObjects[0]);
                            searchModeActive = true;
                            break;
                        case ConsoleKey.A:
                            try
                            {
                                Console.Clear();
                                Console.Write("");
                                List<string> topicParameters = AskTaskParameters();
                                this.ObjectStorage.AddTaskToTopic(topicObjects[currentMenuItem - 1].id, topicParameters[0], topicParameters[1], topicParameters[2], DateTime.Parse(topicParameters[3]));
                                Console.Clear();
                            }
                            catch (Exception e)
                            {

                                Console.WriteLine(e);
                            }
                            break;

                        case ConsoleKey.E:
                            updateLoop = false;
                            break;
                        default:
                            break;
                    }
                }

                if (searchModeActive == true)
                {
                    Thread t = new Thread(new ThreadStart(ThreadInputTest));
                    t.Start();
                    while (true)
                    {
                        int unfilteredTopicListLength = this.ObjectStorage.GetAllTopics().Count;
                        List<Topic> filteredTopicObjects = this.ObjectStorage.GetAllTopicsTitlesMatching(this.searhcstr);
                       // if (filteredTopicObjects.Count == 0)
                       // {
                       //     Console.Clear();
                        //    Console.WriteLine("tyhjä");
                       // }
                        
                        
                        DrawTopicTableWithSearch(cursorInitialLeftPos, cursorInitialTopPos, currentMenuItem, filteredTopicObjects, unfilteredTopicListLength);
                        //Thread.Sleep(400);
                        
                    }
                }

                else
                {
                topicObjects = this.ObjectStorage.GetAllTopics();
                DrawTopicTable(cursorInitialLeftPos, cursorInitialTopPos, currentMenuItem, topicObjects);
                //Vähennä ruudun välkkymistä, mutta aiheuttaa lagia näppäinkomentoihin
                Thread.Sleep(40);

                }

                
            }
        }

        private void ThreadInputTest()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo name = Console.ReadKey(false);


                    if (name.Key == ConsoleKey.Backspace)
                    {
                        this.searhcstr = "";
                        
                    }
                    else
                    {
                        this.searhcstr += name.KeyChar.ToString();
                    }
                    
                    //Console.WriteLine(searchstr);

                }
            }
        }

        private static void DrawTopicTable(int tableStartLeft, int tableStartTop, int selectedRow, List<Topic> topics)
        {
            //Kursori aina samaan paikkaan piirron alussa
            int rowsToDraw = topics.Count;
            int consoleRowBeingDrawn = tableStartTop;
            int currentLogicalRow = 1;
            bool colorThisRow;
            foreach (Topic item in topics)
            {
                
                //Jos ikkunaa scrollaa ja yrittää asettaa cursoria näkyvän osan ulkopuolelle SetCursorPosition metodi asettaa origon scrollatun ikkunan mukaisesti
                Console.SetCursorPosition(0, consoleRowBeingDrawn);
                if (currentLogicalRow == selectedRow)
                {
                    colorThisRow = true;
                }
                else
                {
                    colorThisRow = false;
                }
                PrintLearningTopicRow(item, colorThisRow);
                if (rowsToDraw == currentLogicalRow)
                {
                    
                    return;   
                }
                consoleRowBeingDrawn++;
                currentLogicalRow++;
            }
        }

        private static void PrintLearningTopicRow(Topic item, bool colorRow)
        {
            var stringBuilder = new StringBuilder();
            Console.Write(Console.CursorLeft);
            foreach (var property in item.GetType().GetProperties())
            {
                string prop = property.GetValue(item).ToString();

                if (prop.IndexOf("Task") != -1)
                {
                    stringBuilder.Append(StringFormatterToTable(item.TasksRelatedToTopic.Count.ToString()));
                }

                else if (property.Name.IndexOf("TimeSpent") != -1)
                {
                    double converted = Convert.ToDouble(prop);
                    stringBuilder.Append(StringFormatterToTable(Math.Round(converted).ToString() + "s"));
                }

                else
                {
                    stringBuilder.Append(StringFormatterToTable(prop));
                }

            }
            if (colorRow == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.Write(stringBuilder.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void DrawTopicTableWithSearch(int tableStartLeft, int tableStartTop, int selectedRow, List<Topic> topics, int unfilteredTopicListLength)
        {
            //Kursori aina samaan paikkaan piirron alussa
            
            int rowsToDraw = topics.Count;
            int consoleRowBeingDrawn = tableStartTop;
            int currentLogicalRow = 1;
            bool colorThisRow;
            int blankRowsToDraw = unfilteredTopicListLength - rowsToDraw;
            foreach (Topic item in topics)
            {
                
                //Jos ikkunaa scrollaa ja yrittää asettaa cursoria näkyvän osan ulkopuolelle SetCursorPosition metodi asettaa origon scrollatun ikkunan mukaisesti
                Console.SetCursorPosition(0, consoleRowBeingDrawn);
                if (currentLogicalRow == selectedRow)
                {
                    colorThisRow = true;
                }
                else
                {
                    colorThisRow = false;
                }
                PrintLearningTopicRow(item, colorThisRow);
                if (rowsToDraw == currentLogicalRow)
                {
                    Console.SetCursorPosition(0, Console.WindowHeight - 10);

                    if (this.searhcstr == "")
                    {
                        Console.Write(new string(' ', Console.BufferWidth));
                    }
                    else
                    {
                        Console.Write(this.searhcstr);
                    }

                    for (int i = consoleRowBeingDrawn+1; i <= consoleRowBeingDrawn+blankRowsToDraw; i++)
                    {
                        Console.SetCursorPosition(0, i);
                        Console.Write(new string(' ', Console.BufferWidth));
                    }

                    return;
                }
                consoleRowBeingDrawn++;
                currentLogicalRow++;
            }
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