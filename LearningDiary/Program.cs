﻿using System;
using System.IO;

namespace LearningDiary
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
            // Rivien tulostus ei näytä oikealta <27" näytöllä
            Console.SetWindowSize(Console.LargestWindowWidth, 40);
            Console.BufferHeight = 40;
            Console.BufferWidth = 280;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
            
            Console.CursorVisible = false;

            string dbAddress = @"Server=DESKTOP-7BQQ30N\MSSQLSERVER2\;Database=LearningDiary;Trusted_Connection=True;MultipleActiveResultSets=true";



            //string topicFilename = "topics.csv";
            //string topicFilename = "topics.csv";
            string topicFilename = "topics_8999lines.csv";
            string tasksFilename = "tasks.csv";

            //var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToLower();
            string basePath = path.Substring(0,path.IndexOf("learningdiary"));
            string topicPath = @$"{basePath}learningdiary\learningdiary\files\{topicFilename}";
            string taskPath = @$"{basePath}learningdiary\learningdiary\files\{tasksFilename}";
            FileIO TopicFileRepository = new FileIO(topicPath);
            FileIO TaskFileRepository = new FileIO(taskPath);

            LearningDiary Wk32diaryrepo = new LearningDiary(TopicFileRepository,TaskFileRepository);
            LearningDiaryViews views = new LearningDiaryViews();

            Controller LearningDiaryConterller = new Controller(views,Wk32diaryrepo);

            LearningDiaryConterller.Execute();

        }
    }
}
