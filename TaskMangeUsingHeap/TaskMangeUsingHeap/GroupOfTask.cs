using HeapStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMangeUsingHeap
{
    internal class GroupOfTask
    {
        private Comparer comparer = new Comparer();
        private Heap<Tasks> toDolist;
        public GroupOfTask()
        {
            toDolist = new Heap<Tasks>(comparer)
            {
                new Tasks{CreatedTime = DateTime.Parse("2024-1-20 8:15:00 AM"),DueTime=DateTime.Parse("2024-2-28 12:15:00 PM"),
                        Title="Meeting",Description="Interview",Priority=PrioritysOptions.high},
                new Tasks {CreatedTime = DateTime.Parse("2024-1-27 8:15:00 AM"),DueTime=DateTime.Parse("2024-2-03 10:20:00 AM"),
                        Title="Shopping",Description="LapTop",Priority=PrioritysOptions.Medium,
                        DoneTime=DateTime.Parse("2024-1-27 8:20:00 AM")},
                new Tasks {CreatedTime = DateTime.Parse("2024-1-27 8:15:00 AM"),DueTime=DateTime.Parse("2024-2-03 10:20:00 AM"),
                        Title="Shopping",Description="LapTop",Priority=PrioritysOptions.low,
                        DoneTime=DateTime.Parse("2024-1-27 8:20:00 AM")},
                new Tasks{CreatedTime = DateTime.Parse("2024-1-29 8:15:00"),DueTime=DateTime.Parse("2024-2-29 8:30:00 AM"),
                        Title="Bill Paying",Description="CellPhone",Priority=PrioritysOptions.high,
                        DoneTime=DateTime.Parse("2024-1-29 9:15:00")},
                new Tasks{CreatedTime = DateTime.Parse("2024-1-20 8:15:00 AM"),DueTime=DateTime.Parse("2024-2-27 12:15:00 PM"),
                        Title="Meeting",Description="Interview",Priority=PrioritysOptions.high},
            };
        }
        public Tasks lowPriorityTask()
        {
            return toDolist[0];
        }
        public Tasks highPriorityTask()
        {
            Tasks[] task = new Tasks[toDolist.Count];
            for(int i = toDolist.Count-1;i>=0;i--)
            {
               task[i]= toDolist.Remove();
            }
            return task[0];
        }
    }
}
