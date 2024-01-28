using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoListUsingJson.Entities;
using ToDoListUsingJson.Enum;

namespace ToDoListUsingJson.classes
{
    internal class JasonTasks
    {


       public  static void addTaskToDatabase(List<Tasks>input)
        {
            var totaltask = new List<Tasks>();
            if (File.Exists(@"E:\Tasks.json"))
            {
            string contentJsonFile = File.ReadAllText(@"E:\Tasks.json");
            List<Tasks>? tasks = JsonSerializer.Deserialize<List<Tasks>>(contentJsonFile);
            totaltask = tasks.Concat(input).ToList();
            string json = JsonSerializer.Serialize(totaltask);
            File.WriteAllText(@"E:\Tasks.json", json);
            }
           else
           {
            totaltask= input;
            string json = JsonSerializer.Serialize(totaltask);
            File.WriteAllText(@"E:\Tasks.json", json);
           }
        }
        public static Tasks WhichOneTask(string JasonFilePath)
        {
            string contentJsonFile = File.ReadAllText(JasonFilePath);
            List<Tasks>? tasks = JsonSerializer.Deserialize<List<Tasks>>(contentJsonFile);
            return tasks.OrderBy(i=>i.DueTime).ThenByDescending(i=>i.Priority).First();
        }
    }
}
