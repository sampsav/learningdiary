﻿using System;
using System.Collections.Generic;

namespace LearningDiary
{
    class LearningDiary
    {
        //Laajahko toteutus, yritetty tehdä siten, että ohjelmaa voisi laajentaa tulevina viikkoina modulaarisesti -> katsotaan onnistuuko
        //Tukee kaikkien topicien ja taskien lukua "kannasta" sekä uusien topicien ja taskien kirjoituksen "kantaan"
        //Updatejen tekeminen TODO listalla. Onnistuu helpommin oikealla tietokannalla

        private Dictionary<int, Topic> PastLearnings;
        private Dictionary<int, Task> TasksWithoutTopic;
        private HashSet<int> TaskIdList;
        private FileIO PersistentStorageTopics;
        private FileIO PersistentStorageTasks;

        public LearningDiary(FileIO PersistentStorageTopics, FileIO PersistentStorageTasks)
        {
            this.PastLearnings = new Dictionary<int, Topic>();
            this.TasksWithoutTopic = new Dictionary<int, Task>();
            this.TaskIdList = new HashSet<int>();
            this.PersistentStorageTopics = PersistentStorageTopics;
            this.PersistentStorageTasks = PersistentStorageTasks;
           // LoadAllTopicsFromStorage();
            //LoadAllTasksFromStorage();
        }

        public LearningDiary()
        {
            this.PastLearnings = new Dictionary<int, Topic>();
            this.TasksWithoutTopic = new Dictionary<int, Task>();
            this.TaskIdList = new HashSet<int>();
        }

        private void LoadAllTopicsFromStorage()
        {
            List<string[]> allTopics = this.PersistentStorageTopics.GetAll();

            foreach (string[] topicParameters in allTopics)
            {
                int topicId = Convert.ToInt32(topicParameters[8]);
                if (this.PastLearnings.ContainsKey(topicId))
                {
                    throw new ArgumentException($"Unique constrain violation, topicId = {topicId} not unique");
                }

                else
                {

                    Topic newTopic = new Topic(topicId, topicParameters[9], topicParameters[10], Convert.ToDouble(topicParameters[0]), topicParameters[2],
                          Convert.ToDateTime(topicParameters[3]), Convert.ToBoolean(topicParameters[4]), Convert.ToDateTime(topicParameters[5]), Convert.ToBoolean(topicParameters[6]));

                    this.PastLearnings.Add(topicId, newTopic);

                    //Ei toimi object initializer
                    //Topic newTopic = new Topic(
                    //        id = topicId,
                    //        Title = topicParameters[9],
                    //        Description = topicParameters[10],
                    //        EstimatedTimeToMaster = Convert.ToDouble(topicParameters[0]),
                    //        Source = topicParameters[2],
                    //        StartLearningDate = Convert.ToDateTime(topicParameters[3]),
                    //        InProgress = Convert.ToBoolean(topicParameters[4]),
                    //        CompletionDate = Convert.ToDateTime(topicParameters[5]),
                    //        AlreadyStudied = Convert.ToBoolean(topicParameters[6])
                    //        );

                }

            }
        }

        //overloading vai oma metodi AddTopicToDiaryWithAutoGeneratedId?
        //autogenerate taskID
        public void AddTopicToDiary(string title, string description, double estimatedTimeToMaster, string source)
        {
            //Unique ID generointi kannan vastuulle?
            int topicId = PastLearnings.Count;

            AddTopicToDiary(topicId, title, description, estimatedTimeToMaster, source);

        }

        public void AddTopicToDiary(int topicId, string title, string description, double estimatedTimeToMaster, string source)
        {

            if (this.PastLearnings.ContainsKey(topicId))
            {
                throw new ArgumentException($"Unique constrain violation, topicId = {topicId} not unique");
            }

            else
            {
                Topic newTopic = new Topic(topicId, title, description, estimatedTimeToMaster, source);
                this.PastLearnings.Add(topicId, newTopic);
                this.PersistentStorageTopics.Insert(newTopic);
            }

        }

        public void StartTopicById(int topicId)
        {
            if (this.PastLearnings.ContainsKey(topicId))
            {
                this.PastLearnings[topicId].StartLearning();
            }
            else
            {
                throw new ArgumentException($"Task ID not found, Topicid = {topicId}");
            }
        }

        public void FinishTopicById(int topicId)
        {
            if (this.PastLearnings.ContainsKey(topicId))
            {
                this.PastLearnings[topicId].FinishLearning();
            }
            else
            {

                throw new ArgumentException($"Topic ID not found, Topicid = {topicId}");
            }
        }

        public Topic GetTopicById(int topicId)
        {
            if (this.PastLearnings.ContainsKey(topicId))
            {
                return this.PastLearnings[topicId];
            }

            else
            {
                throw new ArgumentException($"Topic ID not found, Topicid = {topicId}");
            }
        }
        public List<Topic> GetAllTopics()
        {
            return new List<Topic>(this.PastLearnings.Values);
        }



        //autogenerate taskID
        public void AddTaskToTopic(int topicId, string title, string description, string notes, DateTime deadline)
        {
            int taskId = this.TaskIdList.Count;
            AddTaskToTopic(taskId, topicId, title, description, notes, deadline);
        }


        public void AddTaskToTopic(int taskId, int topicId, string title, string description, string notes, DateTime deadline)
        {
            if (this.TaskIdList.Contains(taskId))
            {
                throw new ArgumentException($"Unique constrain violation, TaskId = {taskId} not unique");
            }

            else if (this.PastLearnings.ContainsKey(topicId))
            {
                Task newTask = new Task(taskId, topicId, title, description, notes, deadline);
                this.PastLearnings[topicId].TasksRelatedToTopic.Add(newTask);
                this.TaskIdList.Add(taskId);
                this.PersistentStorageTasks.Insert(newTask);
            }
            else
            {
                throw new ArgumentException($"TopickId = {topicId} not found");
            }
        }

        //autogenerate taskID
        public void AddTaskWithoutTopic(string title, string description, string notes, DateTime deadline)
        {
            int taskId = this.TaskIdList.Count;
            AddTaskWithoutTopic(taskId, title, description, notes, deadline);
        }

        public void AddTaskWithoutTopic(int taskId, string title, string description, string notes, DateTime deadline)
        {
            if (!this.TaskIdList.Contains(taskId))
            {
                throw new ArgumentException($"Unique constrain violation, TaskId = {taskId} not unique");
            }

            else
            {
                Task newTask = new Task(taskId, title, description, notes, deadline);
                this.TasksWithoutTopic.Add(taskId, newTask);
                this.TaskIdList.Add(taskId);
            }
        }

        public void FinishTaskById(int taskId)
        {
            if (this.TasksWithoutTopic.ContainsKey(taskId))
            {
                this.TasksWithoutTopic[taskId].FinishTask();
            }
            else
            {
                throw new ArgumentException($"Task ID not found, taskid = {taskId}");

            }
        }

        public List<Task> GetAllTasksRelatedToTopic(int topicId)
        {
            return this.PastLearnings[topicId].TasksRelatedToTopic;
        }
    }
}
