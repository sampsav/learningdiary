using System;
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
            Console.BufferWidth = 280;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
            
            Console.CursorVisible = false;

            string topicFilename = "topics.csv";
            string tasksFilename = "tasks.csv";

            //var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToLower();
            string basePath = path.Substring(0,path.IndexOf("learningdiary"));
            string topicPath = @$"{basePath}learningdiary\learningdiary\files\topics.csv";
            string taskPath = @$"{basePath}learningdiary\learningdiary\files\tasks.csv";
            FileIO TopicFileRepository = new FileIO(topicPath);
            FileIO TaskFileRepository = new FileIO(taskPath);

            LearningDiary Wk32diaryrepo = new LearningDiary(TopicFileRepository,TaskFileRepository);

            LearningDiaryUI ConterllerAndUi = new LearningDiaryUI(Wk32diaryrepo);

            ConterllerAndUi.Execute();

        }
    }
}
