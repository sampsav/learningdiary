using System;

namespace LearningDiary
{
    class Task : LearningDiaryItem
    {

        public int TopicId { get; private set; }
        public string Notes { get; private set; }
        public DateTime Deadline { get; private set; }
        //public enum Priority {"high","low"};
        public bool Done { get; private set; }



        public Task(int id, string title, string description, string notes, DateTime deadline) : base(id, title, description)
        {

            this.Notes = notes;
            this.Deadline = deadline;
        }

        public Task(int topicId) : this(new Task())
        {
            this.TopicId = topicId;
        }

        public void FinishTask()
        {
            if (!this.Done)
            {
                this.Done = true;
            }
        }
    }
}
