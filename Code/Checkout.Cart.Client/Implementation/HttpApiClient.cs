using System.Collections.Generic;
using System.Configuration;
using Checkout.Cart.Client.Abstraction;
using Newtonsoft.Json;
using RestSharp;

namespace Checkout.Cart.Client.Implementation
{
    public class HttpApiClient : IApiClient
    {
        private readonly IRestClient _client;

        public HttpApiClient()
        {
            var baseApi = ConfigurationManager.AppSettings.Get("baseApi");
            _client = new RestClient(baseApi);
        }

        #region Get-Request

        private IRestResponse Get(string resource, Dictionary<string, string> uriSegments)
        {
            var request = new RestRequest(resource, Method.GET);
            foreach (var uriSegment in uriSegments)
            {
                request.AddUrlSegment(uriSegment.Key, uriSegment.Value);
            }

            return _client.Execute(request);
        }

        public T Get<T>(string resource, Dictionary<string, string> uriSegments)
        {
            var response = Get(resource, uriSegments);

            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<T>(response.Content);

            throw response.ErrorException;
        }

        #endregion


        #region Put-Request

        public T Post<T>(string resource, object body)
        {
            var request = new RestRequest(resource, Method.POST) {RequestFormat = DataFormat.Json};
            return ExecuteRequestWithBody<T>(request, body);
        }

        public T Put<T>(string resource, object body)
        {
            var request = new RestRequest(resource, Method.PUT) { RequestFormat = DataFormat.Json }; ;
            return ExecuteRequestWithBody<T>(request, body);
        }

        public IRestResponse Delete(string resource, object body)
        {
            var request = new RestRequest(resource, Method.DELETE) { RequestFormat = DataFormat.Json }; ;
            return ExecuteRequestWithBody(request, body);
        }

        public IRestResponse Delete(string resource, Dictionary<string, string> uriSegments)
        {
            var request = new RestRequest(resource, Method.DELETE);
            return ExecuteRequestWithUriSegments(request, uriSegments);
        }

        #endregion

        #region Helpers

        private IRestResponse ExecuteRequestWithUriSegments(IRestRequest request,
            Dictionary<string, string> uriSegments)
        {
            foreach (var uriSegment in uriSegments)
            {
                request.AddUrlSegment(uriSegment.Key, uriSegment.Value);
            }

            return _client.Execute(request);
        }

        private IRestResponse ExecuteRequestWithBody(IRestRequest request, object body)
        {
            request.AddBody(body);
            return _client.Execute(request);
        }

        private T ExecuteRequestWithBody<T>(IRestRequest request, object body)
        {
            var response = ExecuteRequestWithBody(request, body);
            return GetContentResponse<T>(response);
        }

        private T GetContentResponse<T>(IRestResponse response)
        {
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<T>(response.Content);
            throw response.ErrorException;
        }

        #endregion
    }
}