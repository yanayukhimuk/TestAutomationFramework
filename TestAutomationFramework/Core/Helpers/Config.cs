using System.Configuration;

namespace TestAutomationFramework.Core.Helpers
{
    public static class Config
    {
        public static string AppUrl => ConfigurationManager.AppSettings["url"];

        public static string Browser => ConfigurationManager.AppSettings["browser"];

        public static string UserName => ConfigurationManager.AppSettings["username"];

        public static string Password => ConfigurationManager.AppSettings["password"];

        public static int WbTimeout => int.Parse(ConfigurationManager.AppSettings["browserWbTimeout"]);
    }
}
