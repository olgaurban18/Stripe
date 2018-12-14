namespace WcfService1
{
    public class Constants
    {
        public static string LoggerPath = "Logger_Path";
        public static string LoggerLevel = "Logger_Level";

        public static string StripeApiKey = "Stripe_ApiKey";

        public static class LogType
        {
            public static string Debug = "DEBUG";
            public static string Info = "INFO";
            public static string Error = "ERROR";
        }

        public static class ChargeStatus
        {
            public static string Succeeded = "succeeded";
            public static string Pending = "pending";
            public static string Failed = "failed";
        }
    }
}