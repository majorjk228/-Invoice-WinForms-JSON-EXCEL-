using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace CourseWork15
{
    internal class MyExcel
    {
        public string path = "";
        public Excel.Application excelApp = new Excel.Application();

        public Excel.Workbook workbook;

        public Excel.Sheets worksheets;
        public Excel.Worksheet worksheet;

        private Excel.Range cell;

        public MyExcel() { }

        public MyExcel(string path)
        {
            this.path = path;

            excelApp.Visible = true;

            workbook = excelApp.Workbooks.Open(path);
            worksheets = workbook.Worksheets;
        }

        public void WriteToCell(int row, int column, int NumberSheet, string data)
        {
            worksheet = worksheets.Item[NumberSheet];

            cell = worksheet.Cells[row, column];
            cell.Value = data;
        }

        public void CleanUp()
        {
            cell = null; worksheets = null; worksheet = null; workbook = null;

            excelApp.Quit();
            excelApp = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        public int CountRows(int NumberSheet)
        {
            worksheet = worksheets.Item[NumberSheet];

            return worksheet.Cells[worksheet.Rows.Count, "A"].End[Excel.XlDirection.xlUp].Row;
        }
    }
}
