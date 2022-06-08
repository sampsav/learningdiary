using System;
namespace LearningDiary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(260, 40);
            LearningDiary Wk32diaryrepo = new LearningDiary();
            Wk32diaryrepo.AddTopicToDiary("testi", "testi", 10, "web");
            Wk32diaryrepo.StartTopicById(0);
            LearningDiaryUI ConterllerAndUi = new LearningDiaryUI(Wk32diaryrepo);

            ConterllerAndUi.Execute();

        }
    }
}
