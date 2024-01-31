using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Todolist.Enums;

namespace Todolist.Classes
{
    internal class GroupOfTask
    {
        private List<Tasks> taskToDo = new List<Tasks>()
        {
            new Tasks() {CreatedTime = DateTime.Parse("2024-1-20 8:15:00 AM"),DueTime=DateTime.Parse("2024-1-30 12:15:00 PM"),
                        Title="Meeting",Description="Interview",Priority=PrioritysOptions.high,
                         },

            new Tasks() {CreatedTime = DateTime.Parse("2024-1-27 8:15:00 AM"),DueTime=DateTime.Parse("2024-1-27 10:20:00 AM"),
                        Title="Shopping",Description="LapTop",Priority=PrioritysOptions.Medium,
                        DoneTime=DateTime.Parse("2024-1-27 8:20:00 AM")},

            new Tasks() {CreatedTime = DateTime.Parse("2024-1-27 8:15:00 AM"),DueTime=DateTime.Parse("2024-1-27 1:30:00 PM"),
                        Title="Meeting",Description="Planning",Priority=PrioritysOptions.low},

            new Tasks() {CreatedTime = DateTime.Parse("2024-1-29 8:15:00"),DueTime=DateTime.Parse("2024-1-29 8:30:00 AM"),
                        Title="Bill Paying",Description="CellPhone",Priority=PrioritysOptions.high,
                        DoneTime=DateTime.Parse("2024-1-29 9:15:00")},
            new Tasks() {CreatedTime = DateTime.Parse("2024-1-30 10:10:00"),DueTime=DateTime.Parse("2024-1-30 8:30:00 AM"),
                        Title="Bill2 Paying",Description="CellPhone",Priority=PrioritysOptions.high,
                        DoneTime=DateTime.Parse("2024-1-30 10:30:00")},

            new Tasks() {CreatedTime = DateTime.Now,DueTime=DateTime.Parse("2024-1-26 6:00:00 PM"),
                        Title="Gym",Description="Run",Priority=PrioritysOptions.high,
                        DoneTime=DateTime.Parse("2024-1-29 8:00:00 AM")},

            new Tasks() {CreatedTime = DateTime.Parse("2024-01-30 12:00:00 PM"),DueTime=DateTime.Parse("2024-1-26 7:30:00 PM"),
                        Title="Meeting",Description="QA",Priority=PrioritysOptions.high,
                          DoneTime=DateTime.Parse("2024-01-30 12:15:00 PM")},
            new Tasks() {CreatedTime = DateTime.Parse("2024-1-23 10:23:00 AM"),DueTime=DateTime.Parse("2024-1-30 12:15:00 PM"),
                        Title="Meeting1",Description="Interview",Priority=PrioritysOptions.high,
                         },
               new Tasks() {CreatedTime = DateTime.Parse("2024-1-23 10:23:00 AM"),DueTime=DateTime.Parse("2024-1-30 12:15:00 PM"),
                        Title="Meeting2",Description="Interview",Priority=PrioritysOptions.high,
                         },
                 new Tasks() {CreatedTime = DateTime.Parse("2024-1-23 10:23:00 AM"),DueTime=DateTime.Parse("2024-1-30 12:15:00 PM"),
                        Title="Meeting3",Description="Interview",Priority=PrioritysOptions.high,
                         },
        };
        public Tasks WhichOneTask() => taskToDo.OrderBy(i => i.DueTime).ThenByDescending(i => i.Priority).First();
        public int SumOfTasks() => taskToDo.Count();
        public int SumOfDoneTask() => taskToDo.Where(i => i.DoneTime is not null).Count();

        public int DoneTasksInRange()
        {
            Console.WriteLine("insert start Date:");
            DateTime start = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("insert start Date:");
            DateTime End = DateTime.Parse(Console.ReadLine());
            return taskToDo.Where(i => isDoneinRange(start, End, i)).Count();
        }
        private bool isDoneinRange(DateTime start, DateTime end, Tasks item) =>
            item.DoneTime is not null && item.DoneTime >= start && item.DoneTime <= end;
        public int ExpiredTask() => taskToDo.Where(i => i.DueTime < DateTime.Now).Count();
        public int UndoneTask() => taskToDo.Where(i => i.DoneTime is null).Count();
        public int UndoneTaskWithPriority()
        {
            Console.WriteLine("Specific Priority");
            PrioritysOptions option = Enum.Parse<PrioritysOptions>(Console.ReadLine());
            return taskToDo.Where(i => i.DoneTime is null && i.Priority == option).Count();
        }
        public void UndoneTaskInLast5Days()
        {

            IEnumerable<Tasks> list = taskToDo.Where(i => i.DoneTime is null && (DateTime.Now.Subtract(i.CreatedTime).TotalDays) > 5).TakeLast(3).ToList();
            foreach (Tasks task in list)
            {
                Console.WriteLine(task);
                Console.WriteLine("=============================================");
            }
        }
        public void DoneTaskInCreatedTime()
        {
            IEnumerable<Tasks> list = taskToDo.Where(i => i.DoneTime is not null && i.DoneTime?.Day == i.CreatedTime.Day && i.DoneTime?.Month == i.CreatedTime.Month).TakeLast(3);
            foreach (Tasks task in list)
            {
                Console.WriteLine(task);
                Console.WriteLine("=============================================");
            }
        }

    }
}

