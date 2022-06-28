using System;

namespace LearningDiary
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime Deadline { get; set; }
        public enum Priority {high,low};
        public bool Done { get; set; }

        public bool Deleted { get; set; }

        public Task() { }
        //public Task(int id, string title, string description, string notes, DateTime deadline)
        //{
        //    this.Notes = notes;
        //    this.Deadline = deadline;
        //}
        //
        //public Task(int id, int topicId, string title, string description, string notes, DateTime deadline)
        //{
        //    this.TopicId = topicId;
        //    this.Notes = notes;
        //    this.Deadline = deadline;
        //}


        public void FinishTask()
        {
            if (!this.Done)
            {
                this.Done = true;
            }
        }
    }
}
