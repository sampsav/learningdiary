using System;

namespace LearningDiary
{
    class LearningDiaryItem
    {
        public int id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        
        public LearningDiaryItem(int id, string title, string description)
        {
            this.id = id;
            this.Title = title;
            this.Description = description;
            

        }
    }
}
