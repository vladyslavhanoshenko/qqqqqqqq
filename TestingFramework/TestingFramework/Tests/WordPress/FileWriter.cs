using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using NUnit.Framework;
using TestingFramework.Helpers;

namespace TestingFramework.Tests.WordPress
{
    [TestFixture]
    class FileWriter
    {
        string emptyFileToRewrite = @"F:\qqqqqqqq\TestingFramework\ToRewrite.txt";
        public string filePath = @"F:\qqqqqqqq\TestingFramework\all2.xlsx";
        string writePath = @"F:\qqqqqqqq\TestingFramework\LoginsAndPasswords.txt";
        ExcelFileReader ExcelReader = new ExcelFileReader();
        ReadersAndWritersToOrFromFile WriteAndReadHelper = new ReadersAndWritersToOrFromFile();

        [Test]
        public void CheckAccountsInFileAndRewrite()
        {
            var dataFromExcel = ExcelReader.ReadExcelFile(filePath);
         

            var dataFromFileWithAccounts = WriteAndReadHelper.ReadFile(writePath).Split('\n');





            int index1 = 0;
            foreach (var field in dataFromFileWithAccounts)
            {
                if (field == String.Empty)
                    continue;

                //var concreteField = field.Replace(';', ':').Remove(0,7);
                if (dataFromExcel.Contains(field))
                {
                    Console.WriteLine();
                }

                int indexInExcelFile = dataFromExcel.FindIndex(i => i.Contains(field));
                if (indexInExcelFile == -1)
                    continue;

                dataFromExcel.Remove(dataFromExcel.ElementAt(indexInExcelFile));
                


            }
            WriteAndReadHelper.RewriteFile(dataFromExcel, emptyFileToRewrite);
        }
    }
}
