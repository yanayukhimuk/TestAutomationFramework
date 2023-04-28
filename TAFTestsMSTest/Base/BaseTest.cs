using Microsoft.Extensions.Logging;
using NLog;
using System.Reflection;
using System.Diagnostics;
using TAFTestsMSTest.Utils;
using TestAutomationFramework.Core.Helpers;
using static TestAutomationFramework.Core.Helpers.WebDriver;
using System.ComponentModel;

namespace TAFTestsMSTest.Base
{
    [TestClass]
    public class BaseTest
    {
        private readonly NLog.Logger? Log = NLog.LogManager.GetCurrentClassLogger();
        protected string TestName => TestContext.TestName;

        public TestContext TestContext { get; set; }    

        [TestInitialize]
        public void TestInitialize()
        {
            Log.Info($"#> Start executing test({TestName}");
            InitializePages(GetType(), new List<FieldInfo>());
            BrowserHelper.OpenBrowser();
            Driver.Navigate().GoToUrl(UrlHelper.Url);
            LogIn();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                MSScreenshoter.SaveScreenShot(TestContext);
            }

            Log.Info($"#> Test finished as '{TestContext.CurrentTestOutcome}'");

            QuitBrowser();
        }
        protected Type? InitializePages(Type type, List<FieldInfo> fields)
        {
            if(type != null) 
            {
                fields.AddRange(type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).ToList());
                if (InitializePages(type.BaseType, fields) == null) return null;
            }

            foreach (var field in fields)
            {
                if (field.GetCustomAttribute(typeof(Page)) != null)
                {
                    field.SetValue(this, Activator.CreateInstance(field.FieldType));
                }
            }

            return type;
        }

        protected virtual void LogIn()
        {
            LoginByCredentials(Config.UserName, Config.Password);   
        }

        protected void LoginByCredentials(string userName, string password)
        {
           
        }
        protected void QuitBrowser()
        {
            Driver.Quit();
            BrowserHelper.Driver = null;
            KillDrivers();
        }

        protected void CloseBrowser()
        {
            Driver.Close();
            BrowserHelper.Driver = null;
            KillDrivers();
        }

        private static void KillDrivers()
        {
            switch (Config.Browser.ToLower())
            {
                case "chrome":
                    KillDriver("chromedriver");
                    break;
                case "edge":
                    KillDriver("msedgekdriver");
                    break;
                default:
                    break;
            }
        }

        private static void KillDriver(string driver)
        {
            try
            {
                Array.ForEach(Process.GetProcessesByName(driver), x => x.Kill());
            }
            
            catch (Win32Exception)
            {

            }
        }
    }
}