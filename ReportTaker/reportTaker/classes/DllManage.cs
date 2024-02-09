using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Hamkara.Contract;
using ReportTaker.Enums;

namespace ReportTaker.classes
{
    public class DllManage
    {
        protected static string ExtensionStatePath = @"ExtensionState.json";
        private static string folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"//plugins";
        protected static List<Assembly> allDlls = new List<Assembly>();
        protected static List<Assembly> dllWithCustomAtt = new List<Assembly>();
        protected static List<Assembly> enableDllWithCustomAtt = new List<Assembly>();
        private static string datapath = @"newTasks.json";
        private static string contentJsonFile = File.ReadAllText(datapath);
        private static List<Tasks> tasks = JsonSerializer.Deserialize<List<Tasks>>(contentJsonFile) ?? new List<Tasks>();
        public static void LoadDlls()
        {
            var dllFiles = Directory.GetFiles(folderPath, "*.dll");
            if (dllFiles.Count() < 10)
            {
                foreach (string assemblyFile in dllFiles)
                {
                    allDlls.Add(Assembly.LoadFile(assemblyFile));
                }
            }
            else
            {
                Parallel.ForEach(dllFiles, (dllFile) =>
                {
                    allDlls.Add(Assembly.LoadFrom(dllFile));
                });
            }
            FindDllWithCustomAtt();
            FindEnabledDllWithCustomAtt();
        }
        public static void FindDllWithCustomAtt()
        {
            dllWithCustomAtt = allDlls.Where(dll => GetDllType(dll).GetCustomAttributes().Any(t => t
                                                  .GetType().Name == "BootcampReportExtensionAttribute"))
                                                   .ToList();
        }
        public static void FindEnabledDllWithCustomAtt()
        {
            Dictionary<string, ExtensionState> document = LoadExtensionStateDocument();
            if (document.Count != 0)
            {
                enableDllWithCustomAtt = dllWithCustomAtt.Where(dll => document[GetDllType(dll).Name] == ExtensionState.enable).ToList();
            }
            else
            {
                enableDllWithCustomAtt = dllWithCustomAtt;
            }
        }

        private static Dictionary<string, ExtensionState> LoadExtensionStateDocument()
        {
            string contentJsonFile = File.ReadAllText(ExtensionStatePath);
            var result = JsonSerializer.Deserialize<Dictionary<string, ExtensionState>>(contentJsonFile) ?? new Dictionary<string, ExtensionState>();
            return result;
        }

        public static CategoryTask GetDllCategory(Assembly assembly)
        {
            Type typeOfAssembly = GetDllType(assembly);
            MethodInfo? CategoryTaskMethod = typeOfAssembly.GetMethod("SetCategory");
            return (CategoryTask)CategoryTaskMethod.Invoke(GetDllInstance(assembly), null);
        }
        public static SubCategory GetDllSubCategory(Assembly assembly)
        {
            Type typeOfAssembly = GetDllType(assembly);
            MethodInfo? SubCategoryMethod = typeOfAssembly.GetMethod("SetSubCategory");
            return (SubCategory)SubCategoryMethod.Invoke(GetDllInstance(assembly), null);
        }
        public static Type GetDllType(Assembly assembly)
        {
            Type[] types = assembly.GetExportedTypes();
            return types[0];
        }
        public static object GetDllInstance(Assembly assembly)
        {
            return Activator.CreateInstance(GetDllType(assembly));
        }

        public static Dictionary<string, int> RunNumericalResultMethod(Assembly assembly)
        {
            Type typeOfAssembly = GetDllType(assembly);
            MethodInfo? numericalResultMethod = typeOfAssembly.GetMethod("NumberResult");
            return numericalResultMethod.Invoke(GetDllInstance(assembly), new object[] { tasks }) as Dictionary<string, int>;
        }
        public static Dictionary<string, List<Tasks>> RunListOfTaskResultMethod(Assembly assembly)
        {
            Type typeOfAssembly = GetDllType(assembly);
            MethodInfo? ListOfTaskResultMethod = typeOfAssembly.GetMethod("ListOfTaskResult");
            return ListOfTaskResultMethod.Invoke(GetDllInstance(assembly), new object[] { tasks }) as Dictionary<string, List<Tasks>>;
        }
        public static Dictionary<string, List<IGrouping<string, Tasks>>> RunGropingResultMethod(Assembly assembly)
        {
            Type typeOfAssembly = GetDllType(assembly);
            MethodInfo? GroupingResultMethod = typeOfAssembly.GetMethod("GroupResult");
            return GroupingResultMethod.Invoke(GetDllInstance(assembly), new object[] { tasks }) as Dictionary<string, List<IGrouping<string, Tasks>>>;
        }
    }
}
