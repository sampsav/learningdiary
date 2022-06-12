using System;

namespace LearningDiary
{
    class Task : LearningDiaryItem
    {

<<<<<<< HEAD
        public int TopicId { get; set; }
        public string Notes { get; set; }
        public DateTime Deadline { get; set; }
        public enum Priority {high,low};
        public bool Done { get; set; }

        public Task(int id, string title, string description, string notes, DateTime deadline) : base(id, title, description)
        {
=======
        public int TopicId { get; private set; }
        public string Notes { get; private set; }
        public DateTime Deadline { get; private set; }
        //public enum Priority {"high","low"};
        public bool Done { get; private set; }



        public Task(int id, string title, string description,string notes,DateTime deadline) :base(id,title,description)
        {
            
>>>>>>> main
            this.Notes = notes;
            this.Deadline = deadline;
        }

<<<<<<< HEAD
        public Task(int id, int topicId, string title, string description, string notes, DateTime deadline) : base(id, title, description)
        {
            this.TopicId = topicId;
            this.Notes = notes;
            this.Deadline = deadline;
        }


=======
        public Task(int topicId) : this(new Task())
        {
            this.TopicId = topicId;
        }

>>>>>>> main
        public void FinishTask()
        {
            if (!this.Done)
            {
                this.Done = true;
            }
        }
    }
}
