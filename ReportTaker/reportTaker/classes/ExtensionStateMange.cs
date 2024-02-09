using Hamkara.Contract;
using ReportTaker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReportTaker.classes
{
    public class ExtensionStateMange : DllManage
    {
        private static List<Assembly> newValidDll = new List<Assembly>();
        private static Dictionary<string, ExtensionState> availableDll = new Dictionary<string, ExtensionState>();

        public static void PrintExtensionState()
        {
            Console.Clear();
            availableDll = ReadExtensionStateDoc();
            DetectNewValidDll();
            if (newValidDll.Any())
            {
                AddNewDll();
                PrintUpdatedExtensionStateDoc();
                Console.WriteLine("\n1.Change state of Extension:");
                Console.WriteLine("2.Back");
                int userInput = Convert.ToInt32(Console.ReadLine());
                switch (userInput)
                {
                    case 1:
                        ChangeStateOfDll();
                        break;
                    case 2:
                        Console.Clear();
                        Screen.PrintMenu();
                        break;
                }
            }
            else
            {
                PrintUpdatedExtensionStateDoc();
                Console.WriteLine("\n1.Change state of Extension:");
                Console.WriteLine("2.Back");
                int userInput = Convert.ToInt32(Console.ReadLine());
                switch (userInput)
                {
                    case 1:
                        ChangeStateOfDll();
                        break;
                    case 2:
                        Console.Clear();
                        Screen.PrintMenu();
                        break;
                }
            }
        }

        private static void ChangeStateOfDll()
        {
            string userInput = "yes";
            int index = 1;
            Dictionary<int, string> showDll = new Dictionary<int, string>();
            foreach (var kv in availableDll)
            {
                showDll.Add(index++, kv.Key);
            }
            while (userInput.Equals("yes"))
            {
                Console.Clear();
                foreach (var kv in showDll)
                {
                    Console.WriteLine($"{kv.Key}. {kv.Value}");
                }
                Console.WriteLine("Which one of Extension you want change?");
                int selectedExtension = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("1.Enable\n2.Disable");
                int selectedState = Convert.ToInt32(Console.ReadLine());
                string dllName = showDll[selectedExtension];
                availableDll[dllName] = (ExtensionState)selectedState;
                Console.WriteLine("Do you want Change anymore?");
                userInput = Console.ReadLine();
            }
            ChangeExtensionStateDoc(availableDll);
            Console.Clear();
            Screen.PrintMenu();
        }

        private static void PrintUpdatedExtensionStateDoc()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Extension Name\t| State");
            foreach (var kv in availableDll)
            {
                Console.WriteLine($"{kv.Key}\t| {kv.Value}");
            }
        }

        private static void AddNewDll()
        {
            Console.Clear();
            int index = 1;
            foreach (var dll in newValidDll)
            {
                Console.WriteLine("These Extension was identified");
                Console.WriteLine($"{GetDllType(dll).Name}");
                Console.WriteLine("1.Enable");
                Console.WriteLine("2.Disable");
                Console.WriteLine("please Select state:");
                int stateinput = Convert.ToInt32(Console.ReadLine());
                availableDll.Add(GetDllType(dll).Name, (ExtensionState)stateinput); ;
                Console.Clear();
            }
            ChangeExtensionStateDoc(availableDll);
        }

        public static List<string> ReturnLoadedDllsName()
        {
            List<string> list = allDlls.Select(dll => GetDllType(dll).Name).ToList();
            return list;
        }
        public static void DetectNewValidDll()
        {
            availableDll = ReadExtensionStateDoc();
            newValidDll = dllWithCustomAtt.Where(dll => !availableDll.ContainsKey(GetDllType(dll).Name)).ToList();
        }
        public static Dictionary<string, ExtensionState> ReadExtensionStateDoc()
        {
            string contentJsonFile = File.ReadAllText(ExtensionStatePath);
            return JsonSerializer.Deserialize<Dictionary<string, ExtensionState>>(contentJsonFile) ?? new Dictionary<string, ExtensionState>();
        }
        public static void ChangeExtensionStateDoc(Dictionary<string, ExtensionState> dic)
        {
            string json = JsonSerializer.Serialize(availableDll);
            File.WriteAllText(ExtensionStatePath, json);
            FindEnabledDllWithCustomAtt();
        }

    }
}
