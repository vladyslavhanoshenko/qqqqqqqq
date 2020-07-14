using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestingFramework.Services.REST.TempMailOrg
{

    public class TempMailApi
    {
        private WebClient client = new WebClient();
        private string BaseUrl = "https://privatix-temp-mail-v1.p.rapidapi.com/request";

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
            return client.DownloadString(BaseUrl + $"/mail/id/{md5Hash}");
        }

        public string GetMailsWithWait(string md5Hash)
        {
            while (true)
            {
                var response = GetEmails(md5Hash);
                if (response.Length > 0)
                    return response;
            }
        }



    }
}
