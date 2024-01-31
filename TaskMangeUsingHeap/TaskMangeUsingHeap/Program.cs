namespace TaskMangeUsingHeap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GroupOfTask TaskList= new GroupOfTask();
            Console.WriteLine($"low priority task:\n{TaskList.lowPriorityTask()}\n\n");
            Console.WriteLine($"high priortity task:\n{TaskList.highPriorityTask()}\n");
        }
    }
}