using System;
namespace LearningDiary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(260, 40);
            
            string topicPath = @"C:\Users\Sampsa\source\repos\LearningDiary\LearningDiary\topics.csv";
            string taskPath = @"C:\Users\Sampsa\source\repos\LearningDiary\LearningDiary\tasks.csv";
            FileIO TopicFileRepository = new FileIO(topicPath);
            FileIO TaskFileRepository = new FileIO(taskPath);

            LearningDiary Wk32diaryrepo = new LearningDiary(TopicFileRepository,TaskFileRepository);

            //Wk32diaryrepo.AddTopicToDiary("testi", "testi", 10, "web");
            //Wk32diaryrepo.StartTopicById(0);
            LearningDiaryUI ConterllerAndUi = new LearningDiaryUI(Wk32diaryrepo);

            ConterllerAndUi.Execute();

        }
    }
}
