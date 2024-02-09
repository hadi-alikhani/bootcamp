using Hamkara.Contract;
using ReportTaker.Entities;
using ReportTaker.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReportTaker.classes
{
    public class Screen
    {
        static List<ReportLog> reportLogs = new List<ReportLog>();
        public static void PrintMenu()
        {
            Console.WriteLine("\t\t=== Bootcamp Report :: An extendible command-line report tool ===\n");
            Console.WriteLine("1. Run Report\n");
            Console.WriteLine("2. Mange Extensions\n");
            Console.WriteLine("3. View Report History\n");
            //detectmethodforinvalidAssembly(); write in menu that for example 2 dll has invalid
            Console.WriteLine("Please input an option:");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    Console.Clear();
                    RunReport();
                    break;
                case "2":
                    ManageExtensions();
                    break;
                case "3":
                    ViewReportHistory();
                    break;
            }
        }

        private static void ViewReportHistory()
        {
            ReportHistoryManage.WriteRportLogToFile(reportLogs, FileType.json);
            ReportHistoryManage.ShowLogReports();
            Console.WriteLine("\n1.Back");
            Console.ReadKey();
            Console.Clear();
            PrintMenu();
        }

        private static void ManageExtensions()
        {
            ExtensionStateMange.PrintExtensionState();
        }

        private static void RunReport()
        {
            CategoryTask categorySelect = default;
            SubCategory subCategorySelect = default;

            Dictionary<int, CategoryTask> showResult = RunReportMange.PrintAvailableCategory();
            foreach (var kv in showResult)
            {
                Console.WriteLine($"{kv.Key}. {kv.Value}");
            }
            int backOption = showResult.Count + 1;
            Console.WriteLine($"\n{backOption}.Back");
            Console.WriteLine("Please insert your Category:");
            int userInput = Convert.ToInt32(Console.ReadLine());
            if (userInput == backOption)
            {
                Console.Clear();
                PrintMenu();
            }
            else
            {
                categorySelect = showResult[userInput];
                Console.Clear();
                Dictionary<int, SubCategory> showSubResult = RunReportMange.PrintAvailableSubCategory(categorySelect);
                foreach (var kv in showSubResult)
                {
                    Console.WriteLine($"{kv.Key}. {kv.Value}");
                }
                int subBackOption = showResult.Count + 1;
                Console.WriteLine($"\n{subBackOption}.Back");
                Console.WriteLine("Please insert your sub Category:");
                int subUserInput = Convert.ToInt32(Console.ReadLine());
                if (subUserInput == backOption)
                {
                    Console.Clear();
                    PrintMenu();
                }
                else
                {
                    subCategorySelect = showSubResult[subUserInput];
                }
            }
            Console.Clear();
            var matchedReport = RunReportMange.FindMatchedReport(categorySelect, subCategorySelect);
            InvokeAllMethodOfReport(matchedReport);
        }
        public static void InvokeAllMethodOfReport(Assembly report)
        {
            Dictionary<string, int> numericalResult = new Dictionary<string, int>();
            Dictionary<string, List<Tasks>> taskResult = new Dictionary<string, List<Tasks>>();
            Dictionary<string, List<IGrouping<string, Tasks>>> GroupingResult = new Dictionary<string, List<IGrouping<string, Tasks>>>();
            try
            {
                numericalResult = DllManage.RunNumericalResultMethod(report);
            }
            catch (Exception e) { }
            try
            {
                taskResult = DllManage.RunListOfTaskResultMethod(report);
            }
            catch (Exception e) { }
            try
            {
                GroupingResult = DllManage.RunGropingResultMethod(report);
            }
            catch (Exception e) { }
            int index = 1;
            Dictionary<int, string> showAvailableReport = new Dictionary<int, string>();
            foreach (var kv in numericalResult)
            {
                showAvailableReport.Add(index++, kv.Key);
            }
            foreach (var kv in taskResult)
            {
                showAvailableReport.Add(index++, kv.Key);
            }
            foreach (var kv in GroupingResult)
            {
                showAvailableReport.Add(index++, kv.Key);
            }
            foreach (var kv in showAvailableReport)
            {
                Console.WriteLine($"{kv.Key}. {kv.Value}");
            }
            int selectedOption = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            string optionToResultTitle = showAvailableReport[selectedOption];
            if (taskResult.ContainsKey(optionToResultTitle))
            {
                reportLogs.Add(new ReportLog { Date = DateTime.Now, ReportTitle = optionToResultTitle });
                Console.WriteLine($"The Result of '{optionToResultTitle}' is \n");
                foreach (var task in taskResult[optionToResultTitle])
                {
                    Console.WriteLine($"{task}");
                    Console.WriteLine("===============");
                }
            }
            else if (numericalResult.ContainsKey(optionToResultTitle))
            {
                reportLogs.Add(new ReportLog { Date = DateTime.Now, ReportTitle = optionToResultTitle });

                Console.WriteLine($"The Result of  '{optionToResultTitle}' is \n {numericalResult[optionToResultTitle]}");

            }
            else
            {
                reportLogs.Add(new ReportLog { Date = DateTime.Now, ReportTitle = optionToResultTitle });
                foreach (var title in GroupingResult[optionToResultTitle])
                {
                    Console.WriteLine(title.Key);
                    Console.WriteLine("===============");
                    foreach (var Tasks in title)
                    {
                        Console.WriteLine(Tasks);
                        Console.WriteLine("**********************");
                    }
                }


            }
            Console.WriteLine("1.Back");
            Console.ReadKey();
            Console.Clear();
            PrintMenu();
        }

    }
}

