using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTaker.Entities

{
    public class ReportLog
    {
        public string ReportTitle { get; set; }
        public DateTime Date { get; set; }

        public override string ToString() =>
                    $"Run '{ReportTitle}' on {Date.ToString("yyyy-MM-dd HH:mm")}";
    }
}
