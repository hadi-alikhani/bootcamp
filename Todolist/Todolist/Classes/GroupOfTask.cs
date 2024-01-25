using System;
using System.Collections.Generic;
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
            new Tasks() {CreatedTime = DateTime.Now,DueTime=DateTime.Parse("2024-1-30"),
                        Title="Meeting",Description="Interview",Priority=PrioritysOptions.high},
            new Tasks() {CreatedTime = DateTime.Now,DueTime=DateTime.Parse("2024-1-29"),
                        Title="Shopping",Description="LapTop",Priority=PrioritysOptions.Medium},
            new Tasks() {CreatedTime = DateTime.Now,DueTime=DateTime.Parse("2024-1-28"),
                        Title="Meeting",Description="Planning",Priority=PrioritysOptions.low},
            new Tasks() {CreatedTime = DateTime.Now,DueTime=DateTime.Parse("2024-1-28"),
                        Title="Bill Paying",Description="CellPhone",Priority=PrioritysOptions.high}

        };
        public Tasks WhichOneTask()=>taskToDo.OrderBy(i=>i.DueTime).ThenByDescending(i=>i.Priority).First();
        

    }
}
