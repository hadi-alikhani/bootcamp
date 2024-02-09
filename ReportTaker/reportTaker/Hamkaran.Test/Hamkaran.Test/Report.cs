using Hamkara.Contract;
using System.Data;

namespace Hamkaran.Test
{
    [BootcampReportExtension]
    public class ReportOne : IReporterExtension
    {
        public Dictionary<string, List<Tasks>>? ListOfTaskResult(List<Tasks> list)
        {
            Dictionary<string, List<Tasks>>? result = new Dictionary<string, List<Tasks>>();
            result.Add("Undone Task in Last Days", OverdueTask(list));
            return result;
        }

        public Dictionary<string, int>? NumberResult(List<Tasks> list)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            result.Add("Count Of Task", CountOfTask(list));
            result.Add("Count Of Overdue Task", CountOfOverdueTask(list));
            return result;
        }

        public CategoryTask SetCategory()
        {
            return CategoryTask.work;
        }
        public int CountOfTask(List<Tasks> list) => list.Count();
        public int CountOfOverdueTask(List<Tasks> list) => list.Where(task => task.DueTime < DateTime.Now&&task.CategoryTask==CategoryTask.work).Count();
        public List<Tasks> OverdueTask(List<Tasks> list) => list.Where(i => i.DueTime < DateTime.Now).ToList();
        public List<IGrouping<string,Tasks>>Groping(List<Tasks>list)=>list.GroupBy(task => task.Title).ToList();

        public Dictionary<string, List<IGrouping<string, Tasks>>>? GroupResult(List<Tasks> list)
        {
            Dictionary<string, List<IGrouping<string, Tasks>>> result = new Dictionary<string, List<IGrouping<string, Tasks>>>();
            result.Add("Groupping Tasks base Title", Groping(list));
            return result;
        }

        public SubCategory SetSubCategory()
        {
            return SubCategory.weekly;
        }
        [AttributeUsage(AttributeTargets.Class)]
        class BootcampReportExtensionAttribute : Attribute
        { }
    }
}