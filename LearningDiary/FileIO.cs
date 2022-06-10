using System;
using System.Collections.Generic;
using System.IO;

namespace LearningDiary
{
    class FileIO
    {
        public string FilePath { get; private set; }
        public FileIO(string filePath)
        {
            this.FilePath = filePath;
        }

        //interface

        public void Insert(LearningDiaryItem newItemToInsert)
        {
            string tempcsvLineToWrite = "";
            string csvLineToWrite = "";
            foreach (var item in newItemToInsert.GetType().GetProperties())
            {
                tempcsvLineToWrite += $"{item.GetValue(newItemToInsert)};";
            }
            //remove last ;
            csvLineToWrite = tempcsvLineToWrite.Remove(tempcsvLineToWrite.Length - 1);
            Console.WriteLine(csvLineToWrite);
            SaveLineToFile(this.FilePath, csvLineToWrite);

        }

        //TODO
        public void Update(LearningDiaryItem itemToUpdate)
        {

        }

        public void Delete(LearningDiaryItem itemToDelete)
        {

        }

        public List<string[]> GetAll()
        {
            string[] fileContent = ReadAllLinesFromFile(this.FilePath);
            List<string[]> diaryItems = new List<string[]>();
            //check if any data besides header row
            if (fileContent.Length > 1)
            {
                int i = 0;
                foreach (string row in fileContent)
                {
                    //skip header
                    if (i == 0)
                    {
                        i++;
                        continue;
                    }
                    string[] separatedFields = row.Split(";");
                    diaryItems.Add(separatedFields);
                }
                return diaryItems;
            }
            else
            {
                return new List<string[]>();

            }
        }

        private string[] ReadAllLinesFromFile(string filepath)
        {

            if (File.Exists(filepath))
            {

                string[] readFromFile = File.ReadAllLines(filepath);
                return readFromFile;
            }
            else
            {
                return Array.Empty<string>();
            }

        }

        private void SaveLineToFile(string filepath, string text)
        {
            if (!File.Exists(filepath))
            {
                Console.WriteLine($"File not found, trying to create it");
            }
            try
            {
                using (StreamWriter shoppingListFile = File.AppendText(filepath))
                {
                    shoppingListFile.WriteLine(text);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }


        }

    }
}
