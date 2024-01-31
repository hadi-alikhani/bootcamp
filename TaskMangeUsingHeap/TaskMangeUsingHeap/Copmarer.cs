using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMangeUsingHeap
{
    internal class Comparer : IComparer<Tasks>
    {
        public int Compare(Tasks? x, Tasks? y)
        {
            if (x.DueTime>y.DueTime) return 1;
            if (x.DueTime<y.DueTime) return -1;
            if (x.DueTime == y.DueTime)
            {
                if(x.Priority<y.Priority) return 1;
                if(x.Priority > y.Priority) return -1;
              
            }   
            return 0;  
        }
    }
}
