using System;
using ClassLibraries;

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

            DateTime futureDate = DateTime.Now.AddDays(1);

            if (DateTools.IsInFuture(futureDate)) {
                Console.WriteLine($"{futureDate} on huomenna ja tulevaisuudessa");
            }
            else if (!DateTools.IsInFuture(futureDate))
            {
            Console.WriteLine($"{futureDate} ei ole tulevaisuudessa");
            }

            //Console.CursorVisible = false;
            //LearningDiary Wk32diaryrepo = new LearningDiary();
            //
            //Wk32diaryrepo.AddTopicToDiary("testi", "testi3", 10, "web");
            //LearningDiaryViews views = new LearningDiaryViews();
            //
            //Controller LearningDiaryConterller = new Controller(views, Wk32diaryrepo);
            //
            //LearningDiaryConterller.Execute();

        }
    }
}
