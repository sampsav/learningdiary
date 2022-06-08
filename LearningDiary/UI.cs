using System;
using System.Collections.Generic;

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
                    Console.Clear();
                    List<Topic> topicObjects = this.ObjectStorage.GetAllTopics();
                    PrintHeading(topicObjects[0]);
                    PrintAllLerningDiaryTopics(topicObjects);
                }

                else if (command == "2")
                {
                    while (true)
                    {
                        Console.WriteLine("Input topic ID, back to main menu with ..");
                        string selection = Console.ReadLine();
                        if (selection == "..")
                        {
                            break;
                        }
                        else
                        {
                            int topicId = Convert.ToInt32(selection);
                            ManipulateTopicState(topicId);

                        }
                    }

                }

                else if (command == "3")
                {


                }

                else if (command == "4")
                {
                    command = Console.ReadLine();
                }

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

        private void ManipulateTopicState(int topicId)
        {

            Topic topicUnderManipulation = this.ObjectStorage.GetTopicById(topicId);

            List<Topic> templist = new List<Topic>();
            templist.Add(topicUnderManipulation);

            Console.WriteLine("Topic state:");
            PrintHeading(topicUnderManipulation);
            PrintAllLerningDiaryTopics(templist);

            Console.WriteLine("Start topic? y/n");
            if (Console.ReadLine() == "y"){this.ObjectStorage.StartTopicById(topicId);}
            
            Console.WriteLine("End topic? y/n");
            if (Console.ReadLine() == "y"){ this.ObjectStorage.FinishTopicById(topicId); }
            
            Console.WriteLine("Add task to topic? y/n");
            if (Console.ReadLine() == "y")
            {
                List<string> topicParameters = AskTaskParameters();
                this.ObjectStorage.AddTaskToTopic(topicId, topicParameters[0], topicParameters[1], topicParameters[2], DateTime.Parse(topicParameters[3]));
            }

            Console.WriteLine("Topic state after manipulation");
            PrintHeading(topicUnderManipulation);
            PrintAllLerningDiaryTopics(templist);

            Console.WriteLine("List of tasks related to topic:");
            PrintHeading(topicUnderManipulation.TasksRelatedToTopic[0]);
            PrintAllTasksRelatedToTopic(topicUnderManipulation.TasksRelatedToTopic);
        }


        private static void PrintInstructions()
        {
            Console.WriteLine("\nUsage instructions:\n");
            Console.WriteLine("0 Exit program");
            Console.WriteLine("1 List all learning topics");
            Console.WriteLine("2 Select to start topic state change");
            Console.WriteLine("3 Add Topic");
            Console.WriteLine("4 List all tasks related to topic");            
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
            Console.Write(str);
            Console.WriteLine("");
        }

        private static void PrintAllLerningDiaryTopics(List<Topic> items)
        {
            string str = "";

            foreach (var item in items)
            {
                foreach (var property in item.GetType().GetProperties())
                {
                    string prop = property.GetValue(item).ToString();

                    if (prop.IndexOf("Task") != -1)
                    {

                        str += StringFormatterToTable(item.TasksRelatedToTopic.Count.ToString());
                    }
                    else
                    {

                        str += StringFormatterToTable(prop);
                    }
                }
            }
            Console.WriteLine(str);
        }
        
        //toistoa
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