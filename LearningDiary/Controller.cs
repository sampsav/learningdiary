using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LearningDiary
{
    public class Controller
    {

        public LearningDiary ObjectStorage;
        public LearningDiaryViews Views;
        public string searhcstr;

        public Controller(LearningDiaryViews views, LearningDiary objectStorage)
        {
            this.Views = views;
            this.ObjectStorage = objectStorage;
            this.searhcstr = "admin";
        }

        public void Execute()
        {
            TopicControls();
        }

        private void TopicControls()
        {
            Console.Clear();
            int printableVisibleRows = 36;
            List<Topic> topicObjects = this.ObjectStorage.GetAllTopics();
            List<Topic> filteredTopics = GetFilteredTopicList(topicObjects, this.searhcstr, printableVisibleRows);

            if (topicObjects.Count == 0)
            {
                Console.WriteLine("No Topics in Learning Diary");
                Thread.Sleep(5000);
                return;
            }

            int lastVisibleRow = Console.BufferHeight - 1;
            int cursorTopicSearcLeftPosition = 0;
            int cursorTopicSearchTopPosition = 0;
            int cursorInitialLeftPos = 0;
            int cursorInitialTopPos = 0;

            int currentMenuItem = 0;
            bool updateLoop = true;
            bool searchModeActive = false;
            bool printHeaderAndInstructions = true;

            int menuRowCount = -1;
            int selectedTopicId = -1;

            while (updateLoop)
            {
                if (printHeaderAndInstructions)
                {
                    Console.SetCursorPosition(0, 0);
                    this.Views.PrintInstructions();
                    this.Views.PrintSearchInstructions();

                    (cursorTopicSearcLeftPosition, cursorTopicSearchTopPosition) = Console.GetCursorPosition();

                    this.Views.PrintHeadingRow(topicObjects);
                    (cursorInitialLeftPos, cursorInitialTopPos) = Console.GetCursorPosition();
                    printableVisibleRows = Console.BufferHeight - cursorInitialTopPos;
                    printHeaderAndInstructions = false;
                }

                if (currentMenuItem >= 0 && currentMenuItem < filteredTopics.Count)
                {
                    menuRowCount = filteredTopics.Count - 1;
                    selectedTopicId = filteredTopics[currentMenuItem].TopicId;
                }
                else if (filteredTopics.Count == 0)
                {
                    menuRowCount = 0;
                }
                else if (currentMenuItem > filteredTopics.Count)
                {
                    currentMenuItem = filteredTopics.Count - 1;
                }


                //non blocking input
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {

                        case ConsoleKey.UpArrow:

                            currentMenuItem--;
                            if (currentMenuItem < 0)
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

                                currentMenuItem = 0;
                                while (Console.KeyAvailable)
                                    Console.ReadKey(false);
                            }
                            break;

                        case ConsoleKey.S:
                            this.ObjectStorage.StartTopicById(selectedTopicId);
                            break;

                        case ConsoleKey.F:
                            this.ObjectStorage.FinishTopicById(selectedTopicId);
                            break;

                        case ConsoleKey.D:
                            try
                            {
                                if (filteredTopics.Count > 0)
                                {
                                    this.ObjectStorage.DeleteTopicById(selectedTopicId);
                                }

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                return;
                            }
                            break;

                        case ConsoleKey.Q:
                            searchModeActive = true;
                            break;
                        case ConsoleKey.A:
                            try
                            {
                                Console.Clear();
                                List<string> topicParameters = this.Views.AskTopicParameters();
                                this.ObjectStorage.AddTopicToDiary(topicParameters[0], topicParameters[1], double.Parse(topicParameters[2]), "web");
                                printHeaderAndInstructions = true;
                                break;

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

                if (searchModeActive)
                {
                    //TopicSearchInput();
                }

                else
                {

                    topicObjects = this.ObjectStorage.GetAllTopics();
                    filteredTopics = GetFilteredTopicList(topicObjects, this.searhcstr, printableVisibleRows);

                    if (filteredTopics.Count > 0)
                    {
                        this.Views.DrawTopicTable(cursorInitialLeftPos, cursorInitialTopPos, currentMenuItem, filteredTopics);
                        this.Views.WriteEmptyLines(cursorInitialTopPos + filteredTopics.Count, lastVisibleRow);
                        //Vähennä ruudun välkkymistä, mutta aiheuttaa lagia näppäinkomentoihin
                        Thread.Sleep(40);

                    }

                    else
                    {
                        Console.SetCursorPosition(cursorInitialLeftPos, cursorInitialTopPos);
                        Console.WriteLine("No topics on list");
                    }
                }
            }
        }

        public static List<Topic> GetFilteredTopicList(List<Topic> allTopics, string searchStr, int qty) {

            List<Topic> filteredTopics = new List<Topic>();

            if (string.IsNullOrEmpty(searchStr))
            {

                filteredTopics = allTopics;
            }

            else
            {
                filteredTopics = allTopics.Where(t => t.Title.Contains(searchStr)).ToList();
            }

            return filteredTopics.Take(qty).ToList();

        }

        //private static void TopicSearchInput()
        //{
        //    Thread t = new Thread(new ThreadStart(ThreadInputTest));
        //    t.Start();
        //    while (true)
        //    {
        //        int unfilteredTopicListLength = this.ObjectStorage.GetAllTopics().Count;
        //        List<Topic> filteredTopicObjects = this.ObjectStorage.GetAllTopicsTitlesMatching(this.searhcstr);
        //        // if (filteredTopicObjects.Count == 0)
        //        // {
        //        //     Console.Clear();
        //        //    Console.WriteLine("tyhjä");
        //        // }
        //    
        //    
        //        this.Views.DrawTopicTableWithSearch(cursorInitialLeftPos, cursorInitialTopPos, currentMenuItem, filteredTopicObjects, unfilteredTopicListLength);
        //        //Thread.Sleep(400);
        //    
        //    }
        //
        //
        //}

        //private void ThreadInputTest()
        //{
        //    while (true)
        //    {
        //        if (Console.KeyAvailable)
        //        {
        //            ConsoleKeyInfo name = Console.ReadKey(false);
        //
        //
        //            if (name.Key == ConsoleKey.Backspace)
        //            {
        //                this.searhcstr = "";
        //
        //            }
        //            else
        //            {
        //                //this.searhcstr += name.KeyChar.ToString();
        //                this.searhcstr += "admin";
        //            }
        //
        //            //Console.WriteLine(searchstr);
        //
        //        }
        //    }
        //}


    }
}
