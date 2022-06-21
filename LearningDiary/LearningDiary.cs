﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LearningDiary
{
    class LearningDiary
    {
      

        public LearningDiary(FileIO PersistentStorageTopics, FileIO PersistentStorageTasks)
        {
            this.PastLearnings = new Dictionary<int, Topic>();
            this.TasksWithoutTopic = new Dictionary<int, Task>();
            this.TaskIdList = new HashSet<int>();
            this.PersistentStorageTopics = PersistentStorageTopics;
            this.PersistentStorageTasks = PersistentStorageTasks;
            LoadAllTopicsFromStorage();
            //LoadAllTasksFromStorage();
        }

        public LearningDiary()
        {
            string dbAddress = @"Server=DESKTOP-7BQQ30N\MSSQLSERVER2\;Database=LearningDiary;Trusted_Connection=True;MultipleActiveResultSets=true";

            using (var ctx = new LearningDiaryContext()) {
                var learningTopic = new Topic { Title = "testi" };
                ctx.Topics.Add(learningTopic);
            }
        }

        private void LoadAllTopicsFromStorage()
        {
            List<Dictionary<string,string>> allTopics = this.PersistentStorageTopics.GetAll();

            foreach (Dictionary<string,string> topicParameters in allTopics)
            {

                int topicId = Convert.ToInt32(topicParameters["id"]);
                if (this.PastLearnings.ContainsKey(topicId))
                {
                    throw new ArgumentException($"Unique constrain violation, topicId = {topicId} not unique");
                }

                else
                {
                    try
                    {
                        Topic newTopic = new Topic()
                        {
                            id = topicId,
                            Title = topicParameters["Title"],
                            Description = topicParameters["Description"],
                            EstimatedTimeToMaster = Convert.ToDouble(topicParameters["EstimatedTimeToMaster"]),
                            Source = topicParameters["Source"],
                            StartLearningDate = Convert.ToDateTime(topicParameters["StartLearningDate"]),
                            InProgress = Convert.ToBoolean(topicParameters["InProgress"]),
                            CompletionDate = Convert.ToDateTime(topicParameters["CompletionDate"]),
                            AlreadyStudied = Convert.ToBoolean(topicParameters["AlreadyStudied"]),
                            TasksRelatedToTopic = new List<Task>()
                    };
                        this.PastLearnings.Add(topicId, newTopic);
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine(e);
                    }
                }
            }
        }


        public void AddTopicToDiary(string title, string description, double estimatedTimeToMaster, string source)
        {
            //Unique ID generointi kannan vastuulle?
            int topicId = PastLearnings.Count+1;

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

        public List<Topic> GetAllTopicsTitlesMatching(string searchPattern)
        {
            List<Topic> allTopics = GetAllTopics();
            List<Topic> topicsMatchingToSearch = allTopics.FindAll(x=> x.Title.ToLower().Contains(searchPattern.ToLower()));
            return topicsMatchingToSearch;
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

        public void DeleteTopicById(int topicId) {

            if (this.PastLearnings.Remove(topicId))
            {
                return;
            }
            else
            {
                throw new ArgumentException($"Topic with, topicid = {topicId} not removed");
            }
        
        }


        public List<Task> GetAllTasksRelatedToTopic(int topicId)
        {
            return this.PastLearnings[topicId].TasksRelatedToTopic;
        }
    }
}
