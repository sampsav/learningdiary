using System;

namespace LearningDiary
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
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
            LearningDiary Wk32diaryrepo = new LearningDiary();
            
            await Wk32diaryrepo.AddTopicToDiary("testi", "testi3", 10, "web");
            LearningDiaryViews views = new LearningDiaryViews();
            
            Controller LearningDiaryConterller = new Controller(views, Wk32diaryrepo);
            
            await LearningDiaryConterller.Execute();

        }
    }
}
