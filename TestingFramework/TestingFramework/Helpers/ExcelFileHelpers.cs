using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingFramework.Models;

namespace TestingFramework.Helpers
{
    public class ExcelFileHelpers
    {
        public List<string> ReadExcelFile(string filePath)
        {
            Application ObjExcel = new Application();
            Workbook ObjWorkBook = ObjExcel.Workbooks.Open(filePath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Worksheet ObjWorkSheet;
            ObjWorkSheet = (Worksheet)ObjWorkBook.Sheets[1];
            int numCol = 1;
            var usedColumn = ObjWorkSheet.UsedRange.Columns[numCol];
            Array myvalues = (Array)usedColumn.Cells.Value2;
            List<string> list = myvalues.OfType<object>().Select(o => o.ToString()).ToList();
            ObjExcel.Quit();
            return list;
        }

        public void WriteDominosDataToExcel(string filePath, DominosCreatedAccountEntity accountData)
        {
            var oXL = new Application();
            oXL.Visible = true;

            var oWB = (_Workbook)(oXL.Workbooks.Add(""));
            var oSheet = (_Worksheet)oWB.ActiveSheet;

            var objectProperties = accountData.GetType().GetProperties().ToList();

            
            //for(int i = 0; i < numberOfProperties.Count - 1; i++)
            //{
            //    oSheet.Cells[1, i + 1] = numberOfProperties[i].Name;
            //}

            foreach(var property in objectProperties)
            {
                oSheet.Cells[1, objectProperties.IndexOf(property)] = property.Name;
            }

            //oSheet.Ce

        }

        public static void WriteDominosAccountsDataToExcel(string filePath, params DominosCreatedAccountEntity[] accountsData)
        {
            var oXL = new Application();
            //oXL.Visible = true;

            var oWB = (_Workbook)(oXL.Workbooks.Add(""));
            var oSheet = (_Worksheet)oWB.ActiveSheet;

            var objectProperties = accountsData.First().GetType().GetProperties().ToList();

            var columnAndIndexesDictionary = new Dictionary<int, string>();

            foreach (var property in objectProperties)
            {
                var indexOfProperty = objectProperties.IndexOf(property)+1;
                oSheet.Cells[1, indexOfProperty] = property.Name;
                columnAndIndexesDictionary.Add(indexOfProperty, property.Name);
            }

            var rowIndex = 2;

            foreach(var accountData in accountsData)
            {
                foreach(var columnName in columnAndIndexesDictionary.Values)
                {
                    var propertyValue = accountData.GetType().GetProperty(columnName).GetValue(accountData);
                    var columnIndex = columnAndIndexesDictionary.Single(i => i.Value.Equals(columnName)).Key;
                    oSheet.Cells[rowIndex, columnIndex] = propertyValue;
                }
                rowIndex++;
            }

            //oXL.Visible = false;
            //oXL.UserControl = false;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            oWB.SaveAs(filePath, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                false, false, XlSaveAsAccessMode.xlNoChange,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            oWB.Close();
            oXL.Quit();
        }

        public static void test(string path)
        {
            var oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = true;

            //Get a new workbook.
            var oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
            var oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

            //Add table headers going cell by cell.
            oSheet.Cells[1, 1] = "First Name";
            oSheet.Cells[1, 2] = "Last Name";
            oSheet.Cells[1, 3] = "Full Name";
            oSheet.Cells[1, 4] = "Salary";

            //Format A1:D1 as bold, vertical alignment = center.
            oSheet.get_Range("A1", "D1").Font.Bold = true;
            oSheet.get_Range("A1", "D1").VerticalAlignment =
                Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            // Create an array to multiple values at once.
            string[,] saNames = new string[5, 2];

            saNames[0, 0] = "John";
            saNames[0, 1] = "Smith";
            saNames[1, 0] = "Tom";

            saNames[4, 1] = "Johnson";

            //Fill A2:B6 with an array of values (First and Last Names).
            oSheet.get_Range("A2", "B6").Value2 = saNames;

            //Fill C2:C6 with a relative formula (=A2 & " " & B2).
            var oRng = oSheet.get_Range("C2", "C6");
            oRng.Formula = "=A2 & \" \" & B2";

            //Fill D2:D6 with a formula(=RAND()*100000) and apply format.
            oRng = oSheet.get_Range("D2", "D6");
            oRng.Formula = "=RAND()*100000";
            oRng.NumberFormat = "$0.00";

            //AutoFit columns A:D.
            oRng = oSheet.get_Range("A1", "D1");
            oRng.EntireColumn.AutoFit();

            oXL.Visible = false;
            oXL.UserControl = false;
            oWB.SaveAs(path, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            oWB.Close();
            oXL.Quit();
        }
    }
}
