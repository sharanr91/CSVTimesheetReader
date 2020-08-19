using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace CSVReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter CSV file Path / Drag and drop the CSV file");
            var filePath = @"" + Console.ReadLine();
            filePath = filePath.Replace("\"", "");
            
            // TODO: check if the file is .csv
            var csvTable = LoadCSV(filePath);

            var reportList = CreateConsultantList(csvTable);

            //TODO: check on How to out put the file data
            foreach (var i in reportList)
            {
                Console.WriteLine($"{i.ProjectName} {i.UserName} {i.BillableHours} {i.NonBillableHours} ");
            }
            Console.ReadLine();

        }

        public static List<ConsultantTimesheetReport> CreateConsultantList(DataTable csvTable)
        {
            var consultantTimesheetReportList = new List<ConsultantTimesheetReport>();
            var projectName = "";
            for (int i=0; i < csvTable.Rows.Count; i++)
            {

                if (!string.IsNullOrEmpty(csvTable.Rows[i][0].ToString()))
                {
                    projectName = csvTable.Rows[i][0].ToString();
                }
                else
                {
                    var username = csvTable.Rows[i][1].ToString();
                    var billableHours = csvTable.Rows[i][3].ToString();
                    var nonBillableHours = csvTable.Rows[i][4].ToString();
                    consultantTimesheetReportList.Add(new ConsultantTimesheetReport
                    {
                        ProjectName = projectName,
                        UserName = username,
                        BillableHours = billableHours,
                        NonBillableHours = nonBillableHours
                    });
                }

            }
            return consultantTimesheetReportList;
        }

        public static DataTable LoadCSV(string filePath)
        {
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(filePath)), true))
            {
                csvTable.Load(csvReader);
            }
            // TODO: return error if it doesnt contain valid data in .csv
            return csvTable;
        }
    }

    public class ConsultantTimesheetReport
    {
        public string ProjectName { get; set; }
        public string UserName { get; set; }
        public string BillableHours { get; set; }
        public string NonBillableHours { get; set; }
    }
}
