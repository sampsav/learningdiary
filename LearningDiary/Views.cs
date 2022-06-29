using ClassLibraries;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDiary
{
    public class LearningDiaryViews
    {
        public string TopicSearchStr { get; set; }
        public LearningDiaryViews()
        {
            TopicSearchStr = "";
        }

        public void PrintInstructions()
        {
            Console.WriteLine("Usage Instructions:\nArrows: Cycle topic list, S: Start selected topic, F: Finish selected topic, " +
                    "A: Add new topic, E:Exit program, D: remove topic , Q: activate topic search mode");
        }

        public void PrintSearchInstructions()
        {
            Console.Write("Input topic description to search: (activate with Q, accept search str with Enter): ");

        }

        public void PrintHeadingRow(List<Topic> topicObjects)
        {
            PrintHeading(topicObjects[0]);
        }



        public void DrawTopicTable(int tableStartLeft, int tableStartTop, int selectedRow, List<Topic> topics)
        {
            //Kursori aina samaan paikkaan piirron alussa
            int rowsToDraw = topics.Count;
            int consoleRowBeingDrawn = tableStartTop;
            int currentLogicalRow = 0;
            bool colorThisRow;
            Console.SetCursorPosition(0, tableStartTop);
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
            foreach (var property in item.GetType().GetProperties())
            {
                string prop = property.GetValue(item).ToString();

                if (prop.IndexOf("Task") != -1)
                {
                    stringBuilder.Append(StringFormatterToTable(item.Tasks.Count.ToString()));
                }

                else if (property.Name.IndexOf("TimeSpent") != -1)
                {
                    double converted = Convert.ToDouble(prop);
                    TimeSpan t = TimeSpan.FromSeconds(converted);

                    string formattedTimeSpent = string.Format("{0:D2}d:{1:D2}h:{2:D2}m:{3:D2}s",
                                    t.Days,
                                    t.Hours,
                                    t.Minutes,
                                    t.Seconds);

                    //class method käyttö
                    if (!DateTools.IsInTime(item.StartLearningDate.AddHours(item.EstimatedTimeToMaster)) && item.StartLearningDate > DateTime.MinValue)
                    {
                        stringBuilder.Append(StringFormatterToTable(formattedTimeSpent + "!!"));
                    }
                    else
                    {
                    stringBuilder.Append(StringFormatterToTable(formattedTimeSpent));

                    }
                }

                else
                {
                    stringBuilder.Append(StringFormatterToTable(prop));
                }

            }
            if (colorRow)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.Write(stringBuilder.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void WriteEmptyLines(int lastRowDrawn, int lastRowVisible)
        {


            for (int i = lastRowDrawn; i < lastRowVisible; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.BufferWidth));
            }

        }

        public void DrawUserSearchInputText(int searchStartLeftPosition, int searchStartTopPosition, string userInput)
        {

            Console.SetCursorPosition(searchStartLeftPosition, searchStartTopPosition);

            if (userInput == "")
            {
                Console.Write(new string(' ', Console.BufferWidth - searchStartLeftPosition));
            }
            else
            {
                Console.Write(userInput);
            }
        }


        public List<string> AskTopicParameters()
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

        public static List<string> AskTaskParameters()
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

        private static void PrintHeading(object item)
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