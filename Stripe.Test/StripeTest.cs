using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Stripe;
using File = System.IO.File;

namespace WcfService1.Test
{
    [TestClass]
    public class StripeTest
    {
        private readonly IStripe _stripeMock;
        private readonly Mock<ILogger> _loggerMock = new Mock<ILogger>();
        private readonly Mock<IWriter> _writerMock = new Mock<IWriter>();

        public StripeTest()
        {
            StripeConfiguration.SetApiKey("sk_test_6MeQzmNlL9F3MHqJXiFCtpbZ");

            _loggerMock.Setup(a => a.Debug(It.IsAny<string>()));
            _writerMock.Setup(a => a.WriteToFile(It.IsAny<string>(), It.IsAny<WriterOptions>()));

            _stripeMock = new Stripe(_loggerMock.Object);
        }

        [TestMethod]
        public void IsTrue_Transact_From_Stripe()
        {
            var result = _stripeMock.Transact("cus_E95Ma9iXjkICPK", 100, "usd", "card_1DgnELHfplfRyOdgZGmkTmUs", null);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsFalse_Transact_From_Stripe()
        {
            var result = _stripeMock.Transact("cus_E95Ma9iXjkICPK", 10, "usd", "card_1DgnELHfplfRyOdgZGmkTmUs", null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsTrue_InfoLevel_InfoMethod_From_Logger()
        {
            var path = "\\log-info-test.txt";
            var level = LogLevel.Info;
            var testMessage = "test message";

            var logger = new Logger(new Writer(), path, level);
            logger.Info(testMessage);

            var fullPath = $"{AppDomain.CurrentDomain.BaseDirectory}{path}";
            var result = File.ReadAllText(fullPath);

            File.Delete(fullPath);

            Assert.IsTrue(result.StartsWith("[INFO]"));
            Assert.IsTrue(result.EndsWith($":{testMessage}\r\n"));
        }

        [TestMethod]
        public void IsFalse_FileExist_From_Logger()
        {
            var path = "\\log-debug-test.txt";
            var level = LogLevel.Info;
            var testMessage = "test message";

            var logger = new Logger(new Writer(), path, level);
            logger.Debug(testMessage);

            var fullPath = $"{AppDomain.CurrentDomain.BaseDirectory}{path}";

            Assert.IsFalse(File.Exists(fullPath));
        }
    }
}
