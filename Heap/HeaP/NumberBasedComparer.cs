using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Heap.Program;

namespace Heap
{
    internal class NumberasedCompare:IComparer<int>
    {
        public int Compare(int x,int y)
        {
            if (x>y) return 1;
            if (x==y) return 0;
            return -1;
        }
    }
}
