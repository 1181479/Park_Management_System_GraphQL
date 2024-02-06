using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace Park20.Backoffice.Application.Requests
{
    public static class HttpRequests
    {
        private static IConfiguration configuration;

        internal static void SetConfiguration(IConfiguration config)
        {
            configuration = config;
        }

        internal static async Task<T?> SendHttpPostRequest<T>(string baseUrlKey, string endpointKey)
        {
            return await SendHttpPostRequest<T>(baseUrlKey, endpointKey, null);
        }

        internal static async Task<T?> SendHttpPostRequest<T>(string baseUrlKey, string endpointKey, object? content)
        {
            var config = configuration.GetSection("PaymentEndpoints");

            var baseUrl = config[baseUrlKey];
            var endpoint = config[endpointKey];

            if (string.IsNullOrWhiteSpace(baseUrl) || string.IsNullOrWhiteSpace(endpoint))
                return default;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);

                HttpContent httpContent = content != null
                    ? new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")
                    : default;

                var response = await httpClient.PostAsync(endpoint, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
                else
                {
                    throw new HttpRequestException($"Error: {response.StatusCode}");
                }
            }
        }
    }
}
