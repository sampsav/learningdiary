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

        public string GetCSVRepresentation()
        {
            return $"{this.id};{this.Title};{this.Description};{this.EstimatedTimeToMaster};" +
                $"{this.TimeSpent};{this.Source};{this.StartLearningDate};{this.InProgress};" +
                $"{this.CompletionDate}";
        }
        public override string ToString()
        {
            return $"";
        }
    }
}
