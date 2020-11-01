using OpenQA.Selenium;
using NUnit.Framework;
using TestingFramework.Helpers;
using System.Net.Http;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestingFramework.Tests.WordPress
{
    [TestFixture]
    class HttpRequests
    {
        ExcelFileHelpers ExcelReader = new ExcelFileHelpers();
        ReadersAndWritersToOrFromFile WriteAndReadFile = new ReadersAndWritersToOrFromFile();
        string indexFileReaderPath = @"F:\TestingFramework\IndexFileReader.txt";
        string indexFileWriterPath = @"F:\TestingFramework\IndexFileReader.txt";

        string Url;
        string Login;
        string Password;

        [Test]
       public async System.Threading.Tasks.Task SendHttpRequestToWordPressServerAsync()
        {
            var dataFromExcelFile = ExcelReader.ReadExcelFile(indexFileReaderPath);
            int indexFromFile = WriteAndReadFile.ReadIndexFromFile(indexFileReaderPath);
            dataFromExcelFile.RemoveRange(0, indexFromFile);
            var requestBody = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("log", "yfvalve"),
                new KeyValuePair<string, string>("pwd", "yfvalve.net"),
                new KeyValuePair<string, string>("wp-submit","Log In"),
                new KeyValuePair<string, string>("redirect_to","http://yfvalve.net/wp-admin/"),
                new KeyValuePair<string, string>("testcookie", "1")
            };

            using (HttpClient client = new HttpClient())
            {
                foreach (var field in dataFromExcelFile)
                {
                    var test2 = field.Split(';');
                    Url = "http://" + test2[0];
                    Login = test2[1];
                    Password = test2[2];

                    System.Threading.Tasks.Task<HttpResponseMessage> tmp = client.GetAsync(Url);
                    while (!tmp.IsCompleted)
                    {

                    }
                    var result = tmp.Result;


                    using (HttpResponseMessage response = await client.GetAsync(Url))
                    {
                        if (!response.IsSuccessStatusCode)
                            continue;

                        Console.ReadKey();


                    }
                }
            }
        }
    }
}
