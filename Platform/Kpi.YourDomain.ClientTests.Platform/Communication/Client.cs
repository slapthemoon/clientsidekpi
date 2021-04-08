using System;
using System.Text;
using System.Threading.Tasks;
using Kpi.YourDomain.ClientTests.Model.Platform.Communication;
using RestSharp;
using RestSharp.Authenticators;
using Serilog;

namespace Kpi.YourDomain.ClientTests.Platform.Communication
{
    public class Client : IClient
    {
        private readonly ILogger _logger;
        private readonly IRestClient _restClient;

        public Client(
            ILogger logger,
            IRestClient restClient)
        {
            _logger = logger;
            _restClient = restClient;
        }

        public async Task<IRestResponse> ExecuteAsync(IRestRequest request)
        {
            if (string.IsNullOrEmpty(_restClient.BaseUrl?.ToString()))
            {
                throw new Exception("Base uri was not set.");
            }

            var response = await _restClient.ExecuteAsync<IRestResponse>(request);
            return response;
        }

        public void AddHttpBasicAuthenticator(string userName, string password)
        {
            _restClient.Authenticator = new HttpBasicAuthenticator(userName, password, Encoding.UTF8);
        }

        public void SetBaseUri(string baseUri)
        {
            try
            {
                _logger.Information($"Set Base Uri: {baseUri}.");
                _restClient.BaseUrl = new Uri(baseUri);
            }
            catch (Exception e)
            {
                _logger.Error($"Set {baseUri} Base Uri was not set. Exception: {e}.");
            }
        }
    }
}
