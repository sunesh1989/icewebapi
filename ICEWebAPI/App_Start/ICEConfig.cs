using System.Configuration;

namespace ICEWebAPI.App_Start
{
    public static class ICEConfig
    {
        public static string keyName = string.Empty;
        public static string accessKey = string.Empty;
        public static string baseAddress = string.Empty;
        public static string queueName = string.Empty;
        public static void Config()
        {
            keyName = ConfigurationManager.AppSettings["keyName"];
            accessKey = ConfigurationManager.AppSettings["accessKey"];
            baseAddress = ConfigurationManager.AppSettings["baseAddress"];
            queueName = ConfigurationManager.AppSettings["queueName"];

        }
    }
}