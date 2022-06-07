using System;
using System.Collections.Generic;
namespace LearningDiary
{
    class Topic : LearningDiaryItem
    {
        public double EstimatedTimeToMaster { get; private set; }
        public double TimeSpent { 
            get 
            { 
                if (this.InProgress)
                {
                    return (DateTime.UtcNow - this.StartLearningDate).TotalSeconds;
                }
                else if (this.AlreadyStudied)
                {
                    return (this.CompletionDate - this.StartLearningDate).TotalSeconds; 
                }
                else
                {
                    return 0;
                }
             } 
            private set { } 
        }
        public string Source { get; private set; }
        public DateTime StartLearningDate { get; private set; }
        public bool InProgress{ get; private set; }
        public DateTime CompletionDate { get; private set; }
        public bool AlreadyStudied { get; private set; }

        public List<Task> TasksRelatedToTopic { get; private set; }

        public Topic(int id, string title, string description,double estimatedTimeToMaster, string source) : base(id, title,description)
        {
            this.EstimatedTimeToMaster = estimatedTimeToMaster;
            this.Source = source;
            this.InProgress = false;
            this.AlreadyStudied = false;
        }

        public void StartLearning()
        {
            if (!this.InProgress && !this.AlreadyStudied)
            {
                this.StartLearningDate = DateTime.UtcNow;
                this.InProgress = true;
            }

        }

        public void FinishLearning()
        {
            if (this.InProgress && !this.AlreadyStudied)
            {
                this.CompletionDate = DateTime.UtcNow;
                this.InProgress = false;
                this.AlreadyStudied = true;
            }
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
