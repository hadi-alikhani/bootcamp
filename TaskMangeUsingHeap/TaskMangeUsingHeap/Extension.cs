using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeapStructure;

namespace TaskMangeUsingHeap
{
    internal static class Extension
    {
        public static Heap<T> ToHeap<T>(this IEnumerable<T> source, IComparer<T> comparer)
        {
            var heap = new Heap<T>(comparer);
            foreach (var item in source)
            {
                heap.insert(item);
            }
            return heap;

        }
    }
}
