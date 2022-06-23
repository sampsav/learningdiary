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
            Console.BufferHeight = 40;
            Console.BufferWidth = 280;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
            
            Console.CursorVisible = false;

            

            

            //string topicFilename = "topics.csv";
            //string topicFilename = "topics.csv";
            string topicFilename = "topics_8999lines.csv";
            string tasksFilename = "tasks.csv";
            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToLower();
            string basePath = path.Substring(0,path.IndexOf("learningdiary"));
            string topicPath = @$"{basePath}learningdiary\learningdiary\files\{topicFilename}";
            string taskPath = @$"{basePath}learningdiary\learningdiary\files\{tasksFilename}";

            //var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //FileIO TopicFileRepository = new FileIO(topicPath);
            //FileIO TaskFileRepository = new FileIO(taskPath);

            LearningDiary Wk32diaryrepo = new LearningDiary();

            Wk32diaryrepo.AddTopicToDiary("testi", "testi3", 10, "web");
            LearningDiaryViews views = new LearningDiaryViews();

            Controller LearningDiaryConterller = new Controller(views,Wk32diaryrepo);

            LearningDiaryConterller.Execute();

        }
    }
}
