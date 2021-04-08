using Newtonsoft.Json;
using RestSharp;

namespace Kpi.YourDomain.ClientTests.Platform.Communication.Http
{
    public static class RestSharpExtensions
    {
        public static T GetModel<T>(this IRestResponse response) =>
            JsonConvert.DeserializeObject<T>(response.Content);

        public static IRestRequest AddAuthorizationHeader(this IRestRequest request, string accessToken) =>
            request.AddHeader("Authorization", accessToken);
    }
}
