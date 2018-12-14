using System;
using System.Collections.Generic;
using Castle.Core.Internal;
using Stripe;

namespace WcfService1
{
    public class Stripe : IStripe
    {
        private readonly ILogger _logger;
        private readonly ChargeService _service;

        public Stripe(ILogger logger)
        {
            _logger = logger;
            _service = new ChargeService();  
        }

        public bool Transact(string customerId, double amount, string currency, string cardId, Dictionary<string, string> extraData)
        {
            _logger.Log(new LoggerOptions(LogLevel.Debug,"Transaction started"));
            _logger.Log(new LoggerOptions(LogLevel.Debug, $"Input parameters: customerId = {customerId}, amount = {amount}, currency = {currency}, cardId = {cardId}"));

            if (currency.IsNullOrEmpty())
            {
                _logger.Log(new LoggerOptions(LogLevel.Error, "Currency required!"));
                return false;
            }

            var options = new ChargeCreateOptions
            {
                CustomerId = customerId,
                Amount = (long)Math.Truncate(amount),
                Currency = currency,
                SourceId = cardId
            };

            _logger.Log(new LoggerOptions(LogLevel.Debug, "Charge create options created"));

            try
            {
                var charge = _service.Create(options);

                _logger.Log(new LoggerOptions(LogLevel.Info, $"Transaction created with status \'{charge.Status}\'"));

                return charge.Status != Constants.ChargeStatus.Failed;
            }
            catch (StripeException e)
            {
                _logger.Log(new LoggerOptions(LogLevel.Error, $"StripeException: {e.Message}"));
                return false;
            }
            finally
            {
                _logger.Log(new LoggerOptions(LogLevel.Debug, "Transaction finished"));
            }
        }
    }
}
