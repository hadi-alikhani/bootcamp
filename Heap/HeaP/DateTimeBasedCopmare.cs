using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap
{
    internal class DateTimeBasedCopmare : IComparer<DateTime>
    {
        public int Compare(DateTime x, DateTime y)
        {

            if (x == y) return 0;
            if (x > y) return 1;
            return -1;
        }
    }
}
