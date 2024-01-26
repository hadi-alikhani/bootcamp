using System.Runtime.InteropServices;
using ToDoListUsingJson.classes;
using ToDoListUsingJson.Entities;
using ToDoListUsingJson.Enum;

namespace ToDoListUsingJson
{
    internal class Program
    {
        static void Main(string[] args)
        {
           // JasonTasks jasonTasks = new JasonTasks();
            List<Tasks> taskGroup= new List<Tasks>();
            string input= "yes";
          while (input.Equals("yes"))
            {
                var item = CreateNewTask();
                taskGroup.Add(item);
                Console.WriteLine("Are you sure to add Task?");
                input= Console.ReadLine();
            }
          JasonTasks.addTaskToDatabase(taskGroup);
            Console.WriteLine("Be aware that in the near future you will have to perform the following task");
            Console.WriteLine(JasonTasks.WhichOneTask(@"E:\Tasks.json"));

        }
        static Tasks CreateNewTask()
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

            return new Tasks() { Title=title,Description=Description,DueTime=DueTime,CreatedTime=DateTime.Now,Priority
            =priority};
        }
    }
}