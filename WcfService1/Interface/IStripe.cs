using System.Collections.Generic;
using System.ServiceModel;

namespace WcfService1
{
    [ServiceContract]
    public interface IStripe
    {
        [OperationContract]
        bool Transact(string customerId, double amount, string currency, string cardId, Dictionary<string, string> extraData);
    }
}
