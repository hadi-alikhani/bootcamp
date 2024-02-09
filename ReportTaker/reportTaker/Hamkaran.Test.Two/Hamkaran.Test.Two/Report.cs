using Hamkara.Contract;

namespace Hamkaran.Test.Two
{
    [BootcampReportExtension]
    public class ReportTwo : IReporterExtension
    {
        public Dictionary<string, List<IGrouping<string, Tasks>>>? GroupResult(List<Tasks> list)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<Tasks>>? ListOfTaskResult(List<Tasks> list)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, int>? NumberResult(List<Tasks> list)
        {
            Dictionary<string,int> result = new Dictionary<string,int>();
            result.Add("Count of Overdue Task", CountOfOverDueTask(list));
            return result;
        }
        public CategoryTask SetCategory()
        {
            return CategoryTask.personal;
        }
        public int CountOfOverDueTask(List<Tasks> list) => list.Where(task=>task.DueTime<DateTime.Now&&task.CategoryTask==CategoryTask.personal).Count();

        public SubCategory SetSubCategory()
        {
            return SubCategory.daily;
        }
        [AttributeUsage(AttributeTargets.Class)]
        class BootcampReportExtensionAttribute : Attribute
        { }
    }
}