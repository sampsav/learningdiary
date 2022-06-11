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

        public List<Dictionary<string,string>> GetAll()
        {
            string[] fileContent = ReadAllLinesFromFile(this.FilePath);
            List<Dictionary<string, string>> diaryItems = new List<Dictionary<string, string>>();
            //check if any data besides header row
            if (fileContent.Length > 1)
            {
                string[] headerRow = fileContent[0].Split(";");
                for (int j = 0; j < fileContent.Length; j++)
                {
                    if (j == 0)
                    {
                        continue;
                    }
                    string[] separatedFields = fileContent[j].Split(";");
                    Dictionary<string, string> fieldsAndValues = new Dictionary<string, string>();

                    for (int i = 0; i < separatedFields.Length; i++)
                    {
                        fieldsAndValues.Add(headerRow[i], separatedFields[i]);
                    }
                    diaryItems.Add(fieldsAndValues);
                }
                return diaryItems;
            }
            else
            {
                return new List<Dictionary<string, string>>();

            }
        }

        private static string[] ReadAllLinesFromFile(string filepath)
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

        private static void SaveLineToFile(string filepath, string text)
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
