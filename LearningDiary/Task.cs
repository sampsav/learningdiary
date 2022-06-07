using System;

namespace LearningDiary
{
    class Task : LearningDiaryItem
    {

        public int topicId { get; private set; }

        public Task(int id, string title, string description, int topicId) :base(id,title,description)
        {
            this.topicId = topicId;
        }

        public Task(int id, string title, string description) : base(id, title, description)
        {

        }





        public void FinishTask()
        {
            if (this.InProgress && !this.AlreadyStudied)
            {
                this.CompletionDate = DateTime.UtcNow;
                this.InProgress = false;
                this.AlreadyStudied = true;
            }
        }
    }
}
