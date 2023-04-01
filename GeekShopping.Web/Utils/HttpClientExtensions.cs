using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.Web.Utils
{
    public static class HttpClientExtensions
    {
        private static MediaTypeHeaderValue _contentType = new("application/json");

        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) throw new ApplicationException(
                $"Somethind went wrong calling the API: " +
                $"{response.ReasonPhrase}"
                );
            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(dataAsString,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
        }

        public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T productData)
        {
            var dataAsString = JsonSerializer.Serialize(productData);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = _contentType;
            return httpClient.PostAsync(url, content);
        }
        
        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T productData)
        {
            var dataAsString = JsonSerializer.Serialize(productData);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = _contentType;
            return httpClient.PutAsync(url, content);
        }
    }
}