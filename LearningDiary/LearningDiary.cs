using System;
using System.Collections.Generic;

namespace LearningDiary
{
    class LearningDiary
    {
        //testi
        private Dictionary<int,Topic> PastLearnings;
        private Dictionary<int,Task> Tasks;
        //Could be IO interface
        private object IODevice;
        public LearningDiary(object IODevice)
        {
            this.PastLearnings = new Dictionary<int, Topic>();
            this.Tasks = new Dictionary<int, Task>();
        }

        public LearningDiary()
        {

        }

        private void LoadAllTopicsFromIODevice()
        {

            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SaveAllTopicsToIODevice()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AddTopicToDiary(int id, string title, string description, double estimatedTimeToMaster, string source)
        {
            if (!IsIdInDiary(id))
            {
                Topic newTopic = new Topic(id, title, description, estimatedTimeToMaster, source);
                this.PastLearnings.Add(id, newTopic);
            }
            else
            {
                throw new ArgumentException($"Unique constrain violation, id = {id} not unique");
            }
        }

        public void AddTaskToDiary()
        {

        }

        public void StartTopicById(int id)
        {
            if (IsIdInDiary(id))
            {
                this.PastLearnings[id].StartLearning();
            }
        }

        public void FinishTopicById(int id)
        {
            if (IsIdInDiary(id))
            {
                this.PastLearnings[id].FinishLearning();
            }
        }

        private bool IsIdInDiary(int id)
        {
            if (PastLearnings.ContainsKey(id))
            {
                return true;
            }
            return false;
        }

        public List<string> GetAllItemsCSV()
        {
            List<string> allTopics = new List<string>();

            foreach (Topic topic in this.PastLearnings.Values)
            {
                string retval = topic.GetCSVRepresentation();
                allTopics.Add(retval);
            }


            return allTopics;
        }

        public List<Topic> GetAllItems()
        {
            List<Topic> allDiaryItems = new List<Topic>();

            foreach (Topic item in this.PastLearnings.Values)
            {
                allDiaryItems.Add(item);
            }


            return allDiaryItems;
        }

    }
}
