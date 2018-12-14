namespace WcfService1
{
    public class Constants
    {
        public static string LoggerPath = "Logger_Path";
        public static string StripeApiKey = "Stripe_ApiKey";

        public static class LogLevel
        {
            public static string Debug = "Debug";
            public static string Info = "Info";
            public static string Error = "Error";
        }

        public static class ChargeStatus
        {
            public static string Succeeded = "succeeded";
            public static string Pending = "pending";
            public static string Failed = "failed";
        }
    }
}