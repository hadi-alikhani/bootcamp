using Hamkara.Contract;
using System;
using System.Text;
using System.Text.Json;
using System.Data.SqlClient;
using CsvHelper;
using System.Globalization;
using ReportTaker.Enums;
using ReportTaker.Entities;

namespace ReportTaker.classes
{
    public class ReportHistoryManage
    {
        static FileType storageType = default;
        static string reportLogUsingJsonPath = @"ReportLog.json";
        static string reportLogUsingCsvPath = @"ReportLog.csv";
        static string reportLogUsingTextPath = @"ReportLog.txt";
        static string reportLogConnectionString = @"Data Source=(localdb)\\MSSQLLocalDB;
                                                   Initial Catalog=Report;Integrated Security=True;
                                                   Connect Timeout=30;Encrypt=False;";
        public static void WriteRportLogToFile(List<ReportLog> input, FileType type)
        {
            storageType = type;
            switch (type)
            {
                case FileType.json:
                    WitetoJsonFile(input, reportLogUsingJsonPath);
                    break;
                case FileType.csv:
                    WritetoCsvFile(input, reportLogUsingCsvPath);
                    break;
                case FileType.txt:
                    WritetoTextFile(input, reportLogUsingTextPath);
                    break;
                case FileType.Sql:
                    WritetoSql(input, reportLogConnectionString);
                    break;


            }

        }

        private static void WritetoSql(List<ReportLog> input, string reportLogConnectionString)
        {
            foreach (var item in input)
            {
                using (SqlConnection connection = new SqlConnection(reportLogConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
                     "INSERT INTO [Report] (Title,date) VALUES (@title,@date)", connection))
                    {
                        command.Parameters.AddWithValue("@title", item.ReportTitle);
                        command.Parameters.AddWithValue("@date", item.Date);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
        }

        private static void WritetoTextFile(List<ReportLog> input, string reportLogUsingTextPath)
        {
            using (var writer = new StreamWriter("test.txt", true))
            {
                foreach (var item in input)
                {
                    writer.WriteLine($"{item.ReportTitle},{item.Date}");
                }
            }
        }

        private static void WritetoCsvFile(List<ReportLog> input, string reportLogUsingCsvPath)
        {
            using (var writer = new StreamWriter(reportLogUsingCsvPath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(input);
            }
        }

        private static void WitetoJsonFile(List<ReportLog> input, string reportLogUsingJsonPath)
        {
            if (File.Exists(reportLogUsingJsonPath))
            {
                string contentJsonFile = File.ReadAllText(reportLogUsingJsonPath);
                List<ReportLog>? reportLog = JsonSerializer.Deserialize<List<ReportLog>>(contentJsonFile);
                reportLog = reportLog.Concat(input).ToList();
                string json = JsonSerializer.Serialize(reportLog);
                File.WriteAllText(reportLogUsingJsonPath, json);
            }
            else
            {
                string json = JsonSerializer.Serialize(input);
                File.WriteAllText(reportLogUsingJsonPath, json);
            }
        }

        public static void ShowLogReports()
        {
            Console.Clear();
            Console.WriteLine("History:\n");
            Console.WriteLine("**************************************************");
            switch (storageType)
            {
                case FileType.Sql:
                    ReadFromSql(reportLogConnectionString);
                    break;
                case FileType.csv:
                    ReadFromCsv(reportLogUsingCsvPath);
                    break;
                case FileType.json:
                    ReadFromJson(reportLogUsingJsonPath);
                    break;
                case FileType.txt:
                    ReadFromText(reportLogUsingTextPath);
                    break;
            }

        }

        private static void ReadFromText(string path)
        {
            List<ReportLog> reports = new List<ReportLog>();
            using (var reader = new StreamReader(path))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

                    if (values.Length == 2)
                    {
                        string reportTitle = values[0].Trim();
                        DateTime date = DateTime.Parse(values[1].Trim());

                        reports.Add(new ReportLog { ReportTitle = reportTitle, Date = date });
                    }
                }
            }
            Show(reports);
        }

        private static void ReadFromJson(string path)
        {
            List<ReportLog>? reports = new List<ReportLog>();
            if (!File.Exists(path))
            {
                Console.WriteLine("No Report has been Run yet");
            }
            else
            {
                string contentJsonFile = File.ReadAllText(path);
                reports = JsonSerializer.Deserialize<List<ReportLog>>(contentJsonFile);
            }
            Show(reports);
        }

        private static void ReadFromCsv(string path)
        {
            List<ReportLog> reports = new List<ReportLog>();
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                reports = csv.GetRecords<ReportLog>().ToList();
            }
            Show(reports);
        }

        private static void ReadFromSql(string path)
        {
            List<ReportLog> reports = new List<ReportLog>();
            using (SqlConnection connection = new SqlConnection(path))
            {
                connection.Open();

                string query = "SELECT Title, date FROM Report";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            DateTime date = reader.GetDateTime(1);

                            ReportLog report = new ReportLog
                            {
                                ReportTitle = name,
                                Date = date
                            };
                            reports.Add(report);
                        }
                    }
                }

                connection.Close();
            }
            Show(reports);
        }

        private static void Show(List<ReportLog> reports)
        {
            foreach (var report in reports)
            {
                Console.WriteLine($"{report}");
            }
        }
    }
}

