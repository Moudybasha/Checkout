using System.Collections.Generic;
using RestSharp;

namespace Checkout.Cart.Client.Abstraction
{
    public interface IApiClient
    {
        T Get<T>(string resource, Dictionary<string, string> uriSegments);
        T Post<T>(string resource, object body);
        T Put<T>(string resource, object body);
        IRestResponse Delete(string resource, object body);

        IRestResponse Delete(string resource, Dictionary<string, string> uriSegments);
    }
}