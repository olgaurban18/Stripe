using System;
using System.Threading.Tasks;
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
        public void IsTrue_Info_From_Logger()
        {
            var path = "\\log-test.txt";
            var testMessage = "test message";

            var logger = new Logger(new Writer(), path);
            logger.Info(testMessage);

            Task.WaitAll();

            var fullPath = $"{AppDomain.CurrentDomain.BaseDirectory}{path}";
            var result = File.ReadAllText(fullPath);

            File.Delete(fullPath);

            Assert.IsTrue(result.StartsWith("[Info]"));
            Assert.IsTrue(result.EndsWith($":{testMessage}\r\n"));
        }
    }
}
