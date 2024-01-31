namespace Heap
{
    internal class Program
    {
        public class Employee
        {
            public int id;
            public string name;
        }
        static void Main(string[] args)
        {

            //All of These are For test that heap structure and ToHeap() work correctly or not
            DateTimeBasedCopmare comparer1 = new DateTimeBasedCopmare();
            NumberasedCompare comparer2 = new NumberasedCompare();
            var heap = new Heap<int>(comparer2);
            heap.Insert(10);
            heap.Insert(5);
            heap.Insert(17);
            heap.Insert(4);
            heap.Insert(22);
            heap.Remove();
            heap.Remove();
            Console.WriteLine($"high priority node is : {heap.HighPriorityNode()}");

            foreach (var item in heap)
            {
                Console.WriteLine(item);
            }
            //  List<int> number = new List<int>() { 1, 2, 3, 4 };
            //  var listToHeap = number.ToHeap(comparer2);
            //  foreach (var item in listToHeap)
            //                Console.WriteLine(item);
            // }
            //Console.WriteLine(listToHeap.Contains(1));
            // }
            //Heap<Employee> heap1 = new Heap<Employee>(comparer2)
            //{
            //   new Employee{id=5,name="ali"},
            //    new Employee{id=6,name="ali"}
            //};
            //List<Employee> heap2 = new List<Employee>() { new Employee { id = 7, } };


        }

    }
}