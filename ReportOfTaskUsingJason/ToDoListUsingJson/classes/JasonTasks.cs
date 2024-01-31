using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        private List<Tasks> taskToDo = new List<Tasks>();
        private string JasonFilePath = @"E:\Tasks.json";

        public JasonTasks()
        {
           taskToDo= ReadJsonFile();
        }

        private List<Tasks> ReadJsonFile()
        {
            string contentJsonFile = File.ReadAllText(JasonFilePath);
            return JsonSerializer.Deserialize<List<Tasks>>(contentJsonFile);
        }
        private void WriteJasonFile(List<Tasks> tasks)
        {
            string json = JsonSerializer.Serialize(tasks);
            File.WriteAllText(JasonFilePath, json);
        }
        public int SumOfTasks() => taskToDo.Count();
        public int SumOfDoneTask() => taskToDo.Where(i => i.DoneTime is not null).Count();

        public int DoneTasksInRange()
        {
            Console.WriteLine("insert start Date:");
            DateTime start = DateTime.Parse(Console.ReadLine()).Date;
            Console.WriteLine("insert start Date:");
            DateTime End = DateTime.Parse(Console.ReadLine()).Date;
            return taskToDo.Where(i => isDoneinRange(start, End, i)).Count();
        }
        private bool isDoneinRange(DateTime start, DateTime end, Tasks item) =>
            item.DoneTime is not null && item.DoneTime >= start && item.DoneTime <= end;
        public int ExpiredTask() => taskToDo.Where(i => i.DueTime < DateTime.Now).Count();
        public int UndoneTask() => taskToDo.Where(i => i.DoneTime is null).Count();
        public int UndoneTaskWithPriority()
        {
            Console.WriteLine("Specific Priority");
            PrioritysOptions option = System.Enum.Parse<PrioritysOptions>(Console.ReadLine());
            return taskToDo.Where(i => i.DoneTime is null && i.Priority == option).Count();
        }
        public void UndoneTaskInLast5Days()
        {

            IEnumerable<Tasks> list = taskToDo.Where(i => i.DoneTime is null && (DateTime.Now.Subtract(i.CreatedTime).TotalDays) > 5).TakeLast(3);
            foreach (Tasks task in list)
            {
                Console.WriteLine(task);
                printLine();
            }
        }
        public void DoneTaskInCreatedTime()
        {
            IEnumerable<Tasks> list = taskToDo.Where(i => i.DoneTime is not null && i.DoneTime?.Day == i.CreatedTime.Day && i.DoneTime?.Month == i.CreatedTime.Month).TakeLast(3);
            foreach (Tasks task in list)
            {
                Console.WriteLine(task);
                printLine();
            }
        }
        public void TodayTask()
        {
           
            List<Tasks>? task= taskToDo.Where(i=>i.DueTime.Date==DateTime.Now.Date&&i.DoneTime is null).ToList();
            if (task.Count()==0)
            {
                Console.WriteLine("There is no Task");
                return;
            }
            else
            { 
                Console.WriteLine("These are The Task you must Do:");
                foreach (var item in task)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("Which Task you done Please insert that Description");
                string Description = Console.ReadLine();
                taskToDo=taskToDo.Select(i => i.Description.Equals(Description)?new Tasks { DoneTime = DateTime.Now.Date,
                Description=i.Description,CreatedTime=i.CreatedTime,Priority=i.Priority,DueTime=i.DueTime,Title=i.Title,}:i).ToList();
                WriteJasonFile(taskToDo);
            }
        }

        public void AddNewTaskToDatabase()
        {
            List<Tasks> taskGroup = new List<Tasks>();
            string userInput = "yes";
            while (userInput.Equals("yes"))
            {
                var item = CreateNewTask();
                taskGroup.Add(item);
                Console.WriteLine("Are you sure to add Task?");
                userInput = Console.ReadLine();
            };


            var totaltask = new List<Tasks>();
            if (File.Exists(JasonFilePath))
            {
                List<Tasks>? tasks = ReadJsonFile();
                totaltask = tasks.Concat(taskGroup).ToList();
                WriteJasonFile(totaltask);
            }
            else
            {
                totaltask = taskGroup;
                WriteJasonFile(totaltask);
            }

        }
        private Tasks CreateNewTask()
        {
            Console.WriteLine("Please insert your Task title:");
            string title = Console.ReadLine();
            Console.WriteLine("Please Describe your Task:");
            string Description = Console.ReadLine();
            Console.WriteLine("Please insert DueTime of your Task for example 2000-02-01:");
            DateTime DueTime = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Pleaseinsert priorty of your Task:");
            PrioritysOptions priority = Console.ReadLine() switch
            {
                "high" => PrioritysOptions.high,
                "low" => PrioritysOptions.low,
                "Medium" => PrioritysOptions.Medium,
                _ => throw new Exception("inavalid input")
            }; ;
         

            return new Tasks()
            {
                Title = title,
                Description = Description,
                DueTime = DueTime,
                CreatedTime = DateTime.Now.Date,
                Priority = priority,
                DoneTime = null
            };
        }
        private void printLine()
        {
            Console.WriteLine("=============================================");
        }
    }
}
