using System;
using System.IO;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using TestAutomationFramework.Core.Helpers;

namespace TestAutomationFramework.Core.Helpers
{
    public static class BrowserHelper
    {
        private static string DriverPath => Path.Combine(Directory.GetCurrentDirectory(), "Drivers");
        public static RemoteWebDriver Driver { get; set; }  

        public static void OpenBrowser()
        {
            if (Driver == null)
            {
                switch (Config.Browser.ToLower())
                {
                    case "chrome":
                        Driver = GetChromeDriver();
                        break;
                    case "edge":
                        Driver = GetEdgeDriver();
                        break;
                    default:
                        break;
                }
            }
        }

        public static RemoteWebDriver GetChromeDriver()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments(
                "--start-maximized",
                "--allow-running-insecure-content",
                "--ignore-certificate-errors",
                "--test-type");

            var service = ChromeDriverService.CreateDefaultService(DriverPath);
            var caps = new RemoteSessionSettings(chromeOptions);
            var driver = new RemoteWebDriver(service.ServiceUrl, caps, TimeSpan.FromMinutes(3));
            SetUpTimeouts(driver);
            return driver;
        }

        public static RemoteWebDriver GetEdgeDriver()
        {
            var service = EdgeDriverService.CreateDefaultService(DriverPath, @"msedgedriver.exe");
            service.UseVerboseLogging = true;
            service.Start();

            var options = new EdgeOptions();
            var caps = new RemoteSessionSettings(options);
            var driver = new RemoteWebDriver(service.ServiceUrl, caps);
            SetUpTimeouts(driver);
            driver.Manage().Window.Maximize();
            return driver;
        }
        private static void SetUpTimeouts(RemoteWebDriver driver)
        {
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Config.WbTimeout);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.WbTimeout);
        }
    }
}
