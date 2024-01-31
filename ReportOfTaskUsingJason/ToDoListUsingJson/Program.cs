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
            PrintMenu();
        }
            static void PrintMenu()
            
            {
                JasonTasks todolist = new JasonTasks();
                Console.WriteLine("\t\t\t\t\t\tReport The Tasks");
                Console.WriteLine($"1: The Number of Tasks in List");
                Console.WriteLine($"2: The Number of Task That you Done ");
                Console.WriteLine($"3: The Number of Expired Task");
                Console.WriteLine($"4: The Number of Undone Task with Priority");
                Console.WriteLine($"5: The Done Tasks in specific Range");
                Console.WriteLine($"6: The 3 Undone Task That Pass more than 5 days from Created Time it ");
                Console.WriteLine($"7: The 3 done Task that in the same time created and done ");
                Console.WriteLine($"8: The Number of undone Task");
                Console.WriteLine($"9: Add New Task");
                Console.WriteLine($"10:Today Task");

            Console.WriteLine("please insert your choice");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine(todolist.SumOfTasks());
                        screenMethod();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine(todolist.SumOfDoneTask());
                        screenMethod();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine(todolist.ExpiredTask());
                        screenMethod();
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine(todolist.UndoneTaskWithPriority());
                        screenMethod();
                        break;
                    case "5":
                        Console.Clear();
                        Console.WriteLine(todolist.DoneTasksInRange());
                        screenMethod();
                        break;
                    case "6":
                        Console.Clear();
                        todolist.UndoneTaskInLast5Days();
                        screenMethod();
                        break;
                    case "7":
                        Console.Clear();
                        todolist.DoneTaskInCreatedTime();
                        screenMethod();
                        break;
                    case "8":
                        Console.Clear();
                        Console.WriteLine(todolist.UndoneTask());
                        screenMethod();
                        break;
                    case "9":
                        Console.Clear();
                        todolist.AddNewTaskToDatabase();
                        screenMethod();
                        break;
                    case "10":
                        Console.Clear();
                        todolist.TodayTask();
                        screenMethod();
                         break;
                }
            }
            static void screenMethod()
            {
                Console.ReadKey();
                Console.Clear();
                PrintMenu();
            }
        }
    }