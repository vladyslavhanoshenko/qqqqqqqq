using System;
using System.Collections.Generic;
using System.Net.Http;
using NUnit.Framework;

namespace TestingFramework.Tests.WordPress
{
    [TestFixture]
    class PostRequest
    {
        string url = "http://yfvalve.net/wp-login.php";
        string adminUrl = "http://yfvalve.net/wp-login.php";

        [Test]
        public async System.Threading.Tasks.Task WordPressPostRequestAsync()
        {
            var requestBody = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("log", "yfvalve"),
                new KeyValuePair<string, string>("pwd", "yfvalve.net"),
                new KeyValuePair<string, string>("wp-submit","登录"),
                new KeyValuePair<string, string>("redirect_to","http://yfvalve.net/wp-admin/"),
                new KeyValuePair<string, string>("testcookie", "1")
            };
            HttpContent content = new FormUrlEncodedContent(requestBody);
            using(HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage getResponse = await client.GetAsync(url))
                {
                    using (HttpContent responseContent = getResponse.Content)
                    {
                        Console.WriteLine(getResponse.StatusCode);
                    }

                }
                using (HttpResponseMessage response = await client.PostAsync(url, content))
                {
                    using (HttpContent responseContent = response.Content)
                    {
                        Console.WriteLine(response.StatusCode);
                    }

                }
                using (HttpResponseMessage getResponse = await client.GetAsync(adminUrl))
                {
                    using (HttpContent responseContent = getResponse.Content)
                    {
                        Console.WriteLine(getResponse.StatusCode);
                    }

                }
            }


        }
    }
}
