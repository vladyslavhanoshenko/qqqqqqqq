using System;
using OpenQA.Selenium.Chrome;

namespace TestingFramework.Helpers
{
    class UserAgentsAndProxy
    {
        string userAgent = "--user-agent=Mozilla/5.0 (iPad; CPU OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5355d Safari/8536.25";

        public ChromeOptions SetUpUserAgentForBrowser(string userAgent)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument(userAgent);
            return options;
        }

        public ChromeOptions SetUpUserAgentForBrowser(ChromeOptions options)
        {
            //OpenQA.Selenium.Proxy proxy123 = new OpenQA.Selenium.Proxy();
            //proxy123.HttpProxy = "109.173.124.218:7787";
            //proxy123.FtpProxy = "109.173.124.218:7787";
            //proxy123.SslProxy = "109.173.124.218:7787";
            //options.Proxy = proxy123; */
            return options;
        }

    }
}
