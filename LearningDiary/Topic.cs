using System;
using System.Collections.Generic;
namespace LearningDiary
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double EstimatedTimeToMaster { get; set; }
        public double TimeSpent
        {
            get
            {
                if (this.InProgress)
                {
                    return (DateTime.Now - this.StartLearningDate).TotalSeconds;
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
        public string Source { get; set; }
        public DateTime StartLearningDate { get; set; }
        public bool InProgress { get; set; }
        public DateTime CompletionDate { get; set; }
        public bool AlreadyStudied { get; set; }

        public ICollection<Task> Tasks { get; } = new List<Task>();

        public bool Deleted { get; set; }

        public Topic() { } 

        //public Topic(int id, string title, string description, double estimatedTimeToMaster, string source) : base(id, title, description)
        //{
        //    this.EstimatedTimeToMaster = estimatedTimeToMaster;
        //    this.Source = source;
        //    this.InProgress = false;
        //    this.AlreadyStudied = false;
        //    this.TasksRelatedToTopic = new List<Task>();
        //}

        public void StartLearning()
        {
            if (!this.InProgress && !this.AlreadyStudied)
            {
                this.StartLearningDate = DateTime.Now;
                this.InProgress = true;
            }

        }

        public void FinishLearning()
        {
            if (this.InProgress && !this.AlreadyStudied)
            {
                this.CompletionDate = DateTime.Now;
                this.InProgress = false;
                this.AlreadyStudied = true;
            }
        }

    }
}
