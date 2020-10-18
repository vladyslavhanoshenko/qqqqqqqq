using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestingFramework.Services.REST.TempMailOrg.Models;

namespace TestingFramework.Services.REST.TempMailOrg
{

    public class TempMailApi
    {
        private WebClient client = new WebClient();
        private string BaseUrl = "https://rapidapi.p.rapidapi.com/request";

        public TempMailApi()
        {
            client.Headers.Add("x-rapidapi-host", "privatix-temp-mail-v1.p.rapidapi.com");
            client.Headers.Add("x-rapidapi-key", "8bd9816ab9msh88d4253211b2e9ep1718a7jsn5c2b48b15e05");
        }

        public string[] GetDomainsList()
        {
            var response = client.DownloadString(BaseUrl + "/domains/");
            var test = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(response);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(response);
        }

        private string GetEmails(string md5Hash)
        {
            return client.DownloadString(BaseUrl + $"/mail/id/{md5Hash}/");
        }

        public EmailViewModel[] GetMailsWithWait(string md5Hash)
        {
            string response = GetEmails(md5Hash);
            while (response.Contains("There are no emails yet"))
            {
                response = GetEmails(md5Hash);
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<EmailViewModel[]>(response);
        }
    }
}
