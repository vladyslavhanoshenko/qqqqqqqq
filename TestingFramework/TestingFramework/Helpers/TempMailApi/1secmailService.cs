using System.Collections.Generic;
using System.Net;

namespace TestingFramework.Helpers.TempMailApi
{
    public class _1secmailService
    {
        private static WebClient client = new WebClient();
        private static string BaseUrl = "https://www.1secmail.com/api/v1/?";


        public static MailBoxMessagesModel[] GetMessagesFromMailBox(string mailBoxName, string domainName) 
        {
            var response = WaitResponse(BaseUrl + $"action=getMessages&login={mailBoxName}&domain={domainName}");       
            var convertedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<MailBoxMessagesModel[]>(response);
            return convertedResponse;
        }

        public static SingleMessageModel GetSingleMessage(string mailBoxName, string domainName, int id)
        {
            var response = WaitResponse(BaseUrl + $"action=readMessage&login={mailBoxName}&domain={domainName}&id={id}");
            var convertedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SingleMessageModel>(response);
            return convertedResponse;
        }

        private static string WaitResponse(string url)
        {
            string response = string.Empty;
            while (response == null ||response == "" ||response.Length == 0)
            {
                response = client.DownloadString(url);
            }
            return response;
        }
    }

    public class MailBoxMessagesModel
    {
        public int id { get; set; }
        public string from { get; set; }
        public string subject { get; set; }
        public string date { get; set; }
    }

    public class Attachment
    {
        public string filename { get; set; }
        public string contentType { get; set; }
        public int size { get; set; }
    }

    public class SingleMessageModel
    {
        public int id { get; set; }
        public string from { get; set; }
        public string subject { get; set; }
        public string date { get; set; }
        public List<Attachment> attachments { get; set; }
        public string body { get; set; }
        public string textBody { get; set; }
        public string htmlBody { get; set; }
    }
}
