using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LearningDiary
{
    public class LearningDiary
    {

      
        public LearningDiary()
        {
        }

        //private void LoadAllTopicsFromStorage()
        //{
        //    List<Dictionary<string,string>> allTopics = this.PersistentStorageTopics.GetAll();
        //
        //    foreach (Dictionary<string,string> topicParameters in allTopics)
        //    {
        //
        //        int topicId = Convert.ToInt32(topicParameters["id"]);
        //        if (this.PastLearnings.ContainsKey(topicId))
        //        {
        //            throw new ArgumentException($"Unique constrain violation, topicId = {topicId} not unique");
        //        }
        //
        //        else
        //        {
        //            try
        //            {
        //                Topic newTopic = new Topic()
        //                {
        //                    TopicId = topicId,
        //                    Title = topicParameters["Title"],
        //                    Description = topicParameters["Description"],
        //                    EstimatedTimeToMaster = Convert.ToDouble(topicParameters["EstimatedTimeToMaster"]),
        //                    Source = topicParameters["Source"],
        //                    StartLearningDate = Convert.ToDateTime(topicParameters["StartLearningDate"]),
        //                    InProgress = Convert.ToBoolean(topicParameters["InProgress"]),
        //                    CompletionDate = Convert.ToDateTime(topicParameters["CompletionDate"]),
        //                    AlreadyStudied = Convert.ToBoolean(topicParameters["AlreadyStudied"]),
        //                    TasksRelatedToTopic = new List<Task>()
        //                  };
        //                this.PastLearnings.Add(topicId, newTopic);
        //            }
        //            catch (Exception e)
        //            {
        //
        //                Console.WriteLine(e);
        //            }
        //        }
        //    }
        //}


        public void AddTopicToDiary(string title, string description, double estimatedTimeToMaster, string source)
        {

            using (var context = new LearningDiaryContext())
            {
                Topic newTopic = new Topic()
                {
                    Title = title,
                    Description = description,
                    EstimatedTimeToMaster = estimatedTimeToMaster,
                    Source = source,
                    StartLearningDate = DateTime.MinValue,
                    InProgress = false,
                    CompletionDate = DateTime.MinValue,
                    AlreadyStudied = false,
                };
                context.Topics.Add(newTopic);
                context.SaveChanges();
            }
            }

        public void StartTopicById(int topicId)
        {
            using (var context = new LearningDiaryContext())
            {

                Topic dbTopic = context.Topics.Find(topicId);
                if (dbTopic == null)
                {
                    throw new ArgumentException($"No topic with id {topicId}");
                }
                else { 
                
                dbTopic.StartLearning();
                context.SaveChanges();
                
                }
            }

        }

        public void FinishTopicById(int topicId)
        {

            using (var context = new LearningDiaryContext())
            {

                Topic dbTopic = context.Topics.Find(topicId);
                if (dbTopic == null)
                {
                    throw new ArgumentException($"No topic with id {topicId}");
                }
                else { 
                
                dbTopic.FinishLearning();
                context.SaveChanges();
                }
            }

        }

        public void DeleteTopicById(int topicId)
        {
            using (var context = new LearningDiaryContext())
            {

                Topic dbTopic = context.Topics.Find(topicId);
                if (dbTopic == null)
                {
                    throw new ArgumentException($"No topic with id {topicId}");
                }
                else
                {

                    dbTopic.Deleted = true;
                    context.SaveChanges();
                }
            }

        }

        public List<Topic> GetScreenBufferAmountOfTopics(int sizeOfScreenBuffer)
        {
            using (var context = new LearningDiaryContext())
            {

                List<Topic> topics = context.Topics
                        .Include(e => e.Tasks)
                        .AsNoTracking()
                        .Take(sizeOfScreenBuffer)
                        .ToList();
                return topics;
            }
        }

        public List<Topic> GetAllTopicsTitlesMatching(string searchPattern, int sizeOfScreenBuffer)
        {
            using (var context = new LearningDiaryContext()) { 
            
            List<Topic> topicsMatchingToSearch = context.Topics
                    .Include(e => e.Tasks)
                    .AsNoTracking()
                    .Where(x=> x.Title.ToLower().Contains(searchPattern.ToLower()))
                    .Take(sizeOfScreenBuffer)
                    .ToList();
            return topicsMatchingToSearch;
            }
        }

        //autogenerate taskID
        //public void AddTaskToTopic(int topicId, string title, string description, string notes, DateTime deadline)
        //{
        //    int taskId = this.TaskIdList.Count;
        //    AddTaskToTopic(taskId, topicId, title, description, notes, deadline);
        //}


        //public void AddTaskToTopic(int taskId, int topicId, string title, string description, string notes, DateTime deadline)
        //{
        //    if (this.TaskIdList.Contains(taskId))
        //    {
        //        throw new ArgumentException($"Unique constrain violation, TaskId = {taskId} not unique");
        //    }
        //
        //    else if (this.PastLearnings.ContainsKey(topicId))
        //    {
        //        Task newTask = new Task(taskId, topicId, title, description, notes, deadline);
        //        this.PastLearnings[topicId].TasksRelatedToTopic.Add(newTask);
        //        this.TaskIdList.Add(taskId);
        //        this.PersistentStorageTasks.Insert(newTask);
        //    }
        //    else
        //    {
        //        throw new ArgumentException($"TopickId = {topicId} not found");
        //    }
        //}

        //autogenerate taskID
        //public void AddTaskWithoutTopic(string title, string description, string notes, DateTime deadline)
        //{
        //    int taskId = this.TaskIdList.Count;
        //    AddTaskWithoutTopic(taskId, title, description, notes, deadline);
        //}

        //public void AddTaskWithoutTopic(int taskId, string title, string description, string notes, DateTime deadline)
        //{
        //    if (!this.TaskIdList.Contains(taskId))
        //    {
        //        throw new ArgumentException($"Unique constrain violation, TaskId = {taskId} not unique");
        //    }
        //
        //    else
        //    {
        //        Task newTask = new Task(taskId, title, description, notes, deadline);
        //        this.TasksWithoutTopic.Add(taskId, newTask);
        //        this.TaskIdList.Add(taskId);
        //    }
        //}

        //public void FinishTaskById(int taskId)
        //{
        //    if (this.TasksWithoutTopic.ContainsKey(taskId))
        //    {
        //        this.TasksWithoutTopic[taskId].FinishTask();
        //    }
        //    else
        //    {
        //        throw new ArgumentException($"Task ID not found, taskid = {taskId}");
        //
        //    }
        //}


        //public List<Task> GetAllTasksRelatedToTopic(int topicId)
        //{
        //    return this.PastLearnings[topicId].TasksRelatedToTopic;
        //}
    }
}
