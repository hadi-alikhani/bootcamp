using Hamkara.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReportTaker.classes
{
    public class RunReportMange : DllManage
    {
        public static Dictionary<int, CategoryTask> PrintAvailableCategory()
        {
            Dictionary<int, CategoryTask> showResultInScreen = new Dictionary<int, CategoryTask>();
            List<CategoryTask> Categories = enableDllWithCustomAtt.Select(assembly => GetDllCategory(assembly))
                                                                  .Distinct().ToList();
            int index = 1;
            foreach (CategoryTask category in Categories)
            {
                showResultInScreen.Add(index++, category);
            }
            return showResultInScreen;


        }
        public static Dictionary<int, SubCategory> PrintAvailableSubCategory(CategoryTask userInput)
        {
            Dictionary<int, SubCategory> showResultInScreen = new Dictionary<int, SubCategory>();
            List<SubCategory> subCategories = enableDllWithCustomAtt.Where(dll => GetDllCategory(dll) == userInput).Select(dll => GetDllSubCategory(dll)).ToList();
            int index = 1;
            foreach (SubCategory category in subCategories)
            {
                showResultInScreen.Add(index++, category);
            }
            return showResultInScreen;
        }

        public static Assembly FindMatchedReport(CategoryTask category, SubCategory subcategory)
        {
            return enableDllWithCustomAtt.Find(dll => GetDllCategory(dll) == category && GetDllSubCategory(dll) == subcategory);

        }
    }
}
