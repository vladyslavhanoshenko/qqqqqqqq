using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace TestingFramework.Tests.WordPress
{
    [TestFixture]
    public class InstaChecker
    {
        private string Url = "https://www.instagram.com/accounts/login/ajax/";

        [Test]
        public async System.Threading.Tasks.Task InstaCheckerLoginAsync()
        {
            NameValueCollection loginData = new NameValueCollection
            {
                {"username", "+380638895388"},
                {"password", "asdasdasdassad"},
                {"enc_password", "#PWD_INSTAGRAM_BROWSER:6:1573845563509:AfVQAK/xLqddM+4h245v6EHVeux6W9w36tp22ySQfFw2h6cO5H0fh0vHzHxiEA0AR8gGiSa8B9lMkKqvnCIw83RC6nfLplOuHzuhK4Sptvp5/MGPQw/uHAkb0MetUf9k4Zx8aHLGu+VYgTyFspKnPY6ThxjaKOPjboY=" },
                {"queryParams", "{\"source\":\"auth_switcher\"}"},
                {"optIntoOneTap", "false" }
            };

            var values = new Dictionary<string, string>
            {
                { "username", "+380638895388" },
                { "password", "asdasdasdassad" },
                { "enc_password", ""},
                {"queryParams", "{'source':'auth_switcher'}"},
                {"optIntoOneTap", "false" }

            };

            WebClient client = new WebClient();

            RestClient restClient = new RestClient();

            var content = new FormUrlEncodedContent(values);

            //var response = await restClient.PostAsync("http://www.example.com/recepticle.aspx", content);


            //var responseString = await response.Content.ReadAsStringAsync();


            WebRequest request = (HttpWebRequest)WebRequest.Create(Url);

            client.UploadValues(Url, "POST", loginData);

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                Console.WriteLine("You a fucking loser");
            }


        }

    }

}

