
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using TestingFramework.Helpers;

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

        public enum ChangeStatusCodes
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

        public enum GetStatusCodes
        {
            [Description("STATUS_WAIT_CODE ")]
            STATUS_WAIT_CODE,

            [Description("STATUS_WAIT_RETRY")]
            STATUS_WAIT_RETRY,

            [Description("STATUS_WAIT_RESEND")]
            STATUS_WAIT_RESEND,

            [Description("STATUS_CANCEL")]
            STATUS_CANCEL,

            [Description("STATUS_OK")]
            STATUS_OK
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

        public static string GetPhoneExceptionsParameter(string[] phoneExceptions)
        {
            string stringWithExceptionNumbers = null; 

            foreach(var phone in phoneExceptions)
            {
                stringWithExceptionNumbers += $",{phone}";
            }
            return "&phoneException=" + stringWithExceptionNumbers;
        }

        public static Dictionary<string, string> GetNumber(string serviceName, int forward, string operatorName, int refCode, CountryName country, string[] phoneExceptions)
        {
            var downloadString = $"{BaseUrl}&action=getNumber&service={serviceName}&forward={forward}&country={country}";
            downloadString = refCode == 0 ? downloadString + string.Empty : downloadString + $"&ref={refCode}";
            downloadString = phoneExceptions.Length == 0 ? downloadString + string.Empty : downloadString + GetPhoneExceptionsParameter(phoneExceptions);
            var response = client.DownloadString(downloadString);
            var parsedData = response.Split(':').ToList().Skip(1);
            var data = new Dictionary<string, string>()
            {
                {"Id", parsedData.ElementAt(0)},
                {"PhoneNumber", parsedData.ElementAt(1)}
            };
            return data;
        }

        public static Dictionary<string, string> GetNumberForOtherService(string serviceName = "ot_0", int forward = 0, string operatorName = null, int refCode = 0, 
            CountryName country = CountryName.Ukraine, string[] phoneExceptions = null)
        {
            return GetNumber(serviceName, forward, operatorName, refCode, country, phoneExceptions);
        }

        public static string SetStatus(string status, string id, string forward)
        {
            var response = client.DownloadString(BaseUrl + $"&action=setStatus&status={status}&id={id}&forward={forward}");
            return response;
        }

        public static GetStatusCodes GetStatus(string id)
        {
            var response = client.DownloadString(BaseUrl + $"&action=getStatus&id={id}");
            var test = response.ToEnum<GetStatusCodes>();
            var parsedEnumResponse = Enum.Parse(typeof(GetStatusCodes), response);

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

        //public static string GetStatus(string id)
        //{
        //    string response = string.Empty;
        //    string code = string.Empty;
        //    while (!response.Contains("STATUS_OK"))
        //    {
        //        response = client.DownloadString(BaseUrl + $"&action=getStatus&id={id}");
        //    }
        //    var splittedData = response.Split(':').ToList();
        //    if (splittedData.Count != 3)
        //    {
        //        //добавить запись ошибки в файл
        //        //кинуть  эксепшен
        //    }
        //    code = splittedData.ElementAt(2);
        //    return code;
        //}


    }
}
