using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationFramework.Core.Helpers
{
    public static class WebDriver
    {
        public static RemoteWebDriver Driver => BrowserHelper.Driver;
    }
}
