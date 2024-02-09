using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using ReportTaker.classes;

namespace ReportTaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DllManage.LoadDlls();
            Screen.PrintMenu();
        }

    }
    
}

