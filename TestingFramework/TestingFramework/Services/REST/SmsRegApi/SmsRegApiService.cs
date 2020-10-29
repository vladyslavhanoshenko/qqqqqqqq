
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;

namespace TestingFramework.Services.REST.SmsRegApi
{
    public class SmsRegApiService
    {
        private static string ApiKey = "457311400A97893d7d888ffAbA8e2036";
        private static string BaseUrl = $@"http://sms-activate.ru/stubs/handler_api.php?api_key={ApiKey}";
        private static WebClient client = new WebClient();

        public enum CountryName
        {
            [Description("Ukraine")]
            Ukraine = 1,
        }

        public enum Statuses
        {
            [Description("ACCESS_READY")]
            ACCESS_READY,

            [Description("ACCESS_ACTIVATION")]
            ACCESS_ACTIVATION,

            [Description("ACCESS_CANCEL")]
            ACCESS_CANCEL,

            [Description("ACCESS_RETRY_GET")]
            ACCESS_RETRY_GET

        }

        public enum ErrorStatuses
        {
            ERROR_SQL,
            NO_ACTIVATION,
            BAD_SERVICE,
            BAD_STATUS,
            BAD_KEY,
            BAD_ACTION
        }

        public static double GetBalance()
        {
            var response = client.DownloadString(BaseUrl + "&action=getBalance");
            var value = response.Replace("ACCESS_BALANCE:", string.Empty);
            return double.Parse(value);
        }

        public static JObject GetAvailableNumbers()
        {
            var response = client.DownloadString(BaseUrl + $"&action=getNumbersStatus&country={(int)CountryName.Ukraine}");
            var parsedAvailableNumbers = JObject.Parse(response);
            return parsedAvailableNumbers;
        }

        public static int GetAvailableNumberForOtherServices()
        {
            var parsedAvailableNumbers = GetAvailableNumbers();
            var otherServiceNumbersNode = parsedAvailableNumbers["ot_0"];
            return int.Parse(otherServiceNumbersNode.ToString()); 
        }

        public static Dictionary<string, string> GetNumber(string serviceName, string forward, string operatorName, string refName, string contryName)
        {
            var response = client.DownloadString(BaseUrl + $"&action=getNumber&service={serviceName}&forward={forward}&operator={operatorName}&ref={refName}&country={contryName}");
            var parsedData = response.Split(':').ToList().Skip(1);
            var data = new Dictionary<string, string>()
            {
                {"Id", parsedData.ElementAt(0)},
                {"PhoneNumber", parsedData.ElementAt(1)}
            };
            return data;
        }

        public static Dictionary<string, string> GetNumber(string serviceName = "ot_0", int forward = 0, string operatorName = null, int refCode = 0, 
            CountryName country = CountryName.Ukraine, string[] phoneExceptions = null)
        {
            return null;
        }

        public static string SetStatus(string status, string id, string forward)
        {
            var response = client.DownloadString(BaseUrl + $"&action=setStatus&status={status}&id={id}&forward={forward}");
            return response;
        }

        public static string GetStatus(string id)
        {
            string response = string.Empty;
            string code = string.Empty;
            while (!response.Contains("STATUS_OK"))
            {
                response = client.DownloadString(BaseUrl + $"&action=getStatus&id={id}");
            }
            var splittedData = response.Split(':').ToList();
            if (splittedData.Count != 3)
            {
                //добавить запись ошибки в файл
                //кинуть  эксепшен
            }
            code = splittedData.ElementAt(2);
            return code;
        }


    }
}
