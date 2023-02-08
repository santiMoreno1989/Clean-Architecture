using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using System.Net;
using System.Text;
using System.Text.Json;

namespace CleanArchitecture.Application.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;
        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetResource<T>(string uri, object values = null)
        {
            HttpResponseMessage responseMessage = new();
            try
            {
                responseMessage = await _httpClient.GetAsync(uri);
                responseMessage.EnsureSuccessStatusCode();
                var resultadoStr = await responseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(resultadoStr, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            }
            catch (Exception ex)
            {


                if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                    throw new BadRequestException(ex.Message);

                if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                    throw new KeyNotFoundException();

                throw;
            }
        }

        public async Task<T> PostResource<T>(string url, T content)
        {
            var serialized = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, serialized);
            var errorMessage = $"Request Error  - StatusCode {response.StatusCode.GetHashCode()} {response.ReasonPhrase} {response.RequestMessage.RequestUri}";

            if (!response.IsSuccessStatusCode)
                throw new BadRequestException(errorMessage);

            try
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseBody);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(errorMessage + " - Error al leer la respuesta - " + ex.Message);
            }
        }
    }
}
