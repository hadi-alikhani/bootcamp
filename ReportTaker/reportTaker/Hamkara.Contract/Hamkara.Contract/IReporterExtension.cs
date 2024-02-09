using System.Threading.Tasks;

namespace Hamkara.Contract
{
    public interface IReporterExtension
    {
        public CategoryTask SetCategory();
        public SubCategory SetSubCategory();
        public Dictionary<string, int>? NumberResult(List<Tasks> list);
        public Dictionary<string, List<Tasks>>? ListOfTaskResult(List<Tasks> list);
        public Dictionary <string,List< IGrouping<string, Tasks>>>?  GroupResult(List<Tasks>list);
    }
    public class Tasks
    {
        public DateTime CreatedTime { get; set; }
        public DateTime DueTime { get; set; }
        public required string Description { get; set; }
        public required string Title { get; set; }
        public PrioritysOptions Priority { get; set; }
        public DateTime? DoneTime { get; set; }
        public CategoryTask CategoryTask { get; set; }
        public SubCategory subCategory { get; set; }
        public override string ToString()
        {
            return $"CreatedTime: {CreatedTime}\nDueTime: {DueTime}\nDescription: {Description}\nTitle: {Title}\nPriority: {Priority}";
        }
    }
    public enum PrioritysOptions
    {
        low,
        Medium,
        high
    }
    public enum CategoryTask
    {
        personal,
        work
    }
    public enum SubCategory
    {
        daily,weekly,monthly
    }
}