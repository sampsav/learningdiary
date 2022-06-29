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

        public void FinishTask()
        {
            if (!this.Done)
            {
                this.Done = true;
            }
        }
    }
}
