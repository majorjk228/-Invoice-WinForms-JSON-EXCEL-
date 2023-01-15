using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace CourseWork15
{
    internal class MyExcel
    {
        public string path = "";
        public static Excel.Application excelApp = new Excel.Application();

        public static Excel.Workbook workbook;
        
        public static Excel.Sheets worksheets;
        public static Excel.Worksheet worksheet;

        private static Excel.Range cell;

        public MyExcel() { }

        public MyExcel(string path)
        {
            this.path = path;

            excelApp.Visible = true;

            workbook = excelApp.Workbooks.Open(path);
            worksheets = workbook.Worksheets;
        }

        public void WriteToCell(int row, int column, string data)
        {

            cell = worksheet.Cells[row, column];
            cell.Value = data;
        }

        public string ReadFromCell(int row, int column)
        {
            worksheet = workbook.ActiveSheet;

            cell = worksheet.Cells[row, column];
            string data = Convert.ToString(cell.Value);

            return data;
        }
        public static void CleanUp()
        {
            cell = null; worksheets = null; worksheet = null; workbook = null;

            if (excelApp != null)
            {
                excelApp.Quit();
            }

            excelApp = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void Open()
        {
            workbook = excelApp.Workbooks.Open(@"C:\Users\typakek\Documents\test.xlsx");
            worksheet = workbook.ActiveSheet;
            //excelApp.Visible = true;
        }

        public int CountRows(int NumberSheet)
        {
            worksheet = worksheets.Item[NumberSheet];

            return worksheet.Cells[worksheet.Rows.Count, "A"].End[Excel.XlDirection.xlUp].Row;
        }

        public void Find_Month()
        {
            ResultLoop result = new ResultLoop();

            for (int i = 1; i < worksheet.Cells[worksheet.Rows.Count, "S"].End[Excel.XlDirection.xlUp].Row + 1; i++)
            {                
                string month = ReadFromCell(i, 19);
                
                if (month != "" && month != null)
                {
                    int count = Convert.ToInt32(ReadFromCell(i, 20));
                    result.Results.Add(new Result() { Month = month, Count = count });
                }                
            }
            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            File.WriteAllText(@"results.json", json);            
        }

    }
}
