using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TestAutomationFramework.Core.Helpers.WebDriver;

namespace TAFTestsMSTest.Utils
{
    public static class MSScreenshoter
    {
        private static readonly NLog.Logger? nlogger = NLog.LogManager.GetCurrentClassLogger();
        public static Screenshot GetScreenshot()
        {
            Screenshot? screenshot = null;

            try
            {
                screenshot = TestAutomationFramework.Core.Helpers.WebDriver.Driver.GetScreenshot();
            }

            catch (UnhandledAlertException ex)
            {
                nlogger.Error("Failed to get a screenshot", ex);
                screenshot = TestAutomationFramework.Core.Helpers.WebDriver.Driver.GetScreenshot();
            }

            return screenshot;
        }

        public static void SaveScreenShot(TestContext testContext)
        {
            try
            {
                var filename = $"{testContext.TestName}_{DateTime.UtcNow.ToString("G")}.jpg";
                var path = System.IO.Path.Combine(testContext.ResultsDirectory, filename);
                GetScreenshot().SaveAsFile(path, ScreenshotImageFormat.Jpeg);
                testContext.AddResultFile(path);
            }
            catch (Exception ex)
            {
                nlogger.Error("Failed to save screenshot", ex);
            }
        }
    }
}
