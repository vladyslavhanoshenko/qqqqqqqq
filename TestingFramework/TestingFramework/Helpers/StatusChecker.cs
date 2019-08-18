using System;
using System.Net.Http;

namespace TestingFramework.Helpers
{
    class StatusChecker
    {
        async public void StatusCheck(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                    using (HttpResponseMessage response = await client.GetAsync(url))
                    {
                    if (!response.IsSuccessStatusCode)
                        Console.WriteLine("sucess");

                        Console.ReadKey();


                    }
                }
            

        }
    }
}
