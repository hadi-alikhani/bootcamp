using System;
using System.Text.Json;
using System.Text.RegularExpressions;
using Todolist.Classes;
using Todolist.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Todolist
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GroupOfTask todolist = new GroupOfTask();
           Console.WriteLine(todolist.WhichOneTask());

           
        }
    }
}