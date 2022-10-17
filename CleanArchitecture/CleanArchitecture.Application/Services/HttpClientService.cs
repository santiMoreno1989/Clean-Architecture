using CleanArchitecture.Application.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CleanArchitecture.Application.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient = new();
        private readonly IConfiguration _configuration;

        public HttpClientService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.BaseAddress = new Uri(_configuration.GetSection("ApiAlkemy").Value);
        }

        public async Task<T> GetResource<T>(string url, object values = null)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var resultadoStr = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(resultadoStr, new JsonSerializerOptions {PropertyNameCaseInsensitive=true });
            }
            catch (Exception ex)
            {

                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<T> PostResource<T>(string url, T content)
        {
            var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, serialized);
            var errorMessage = $"Request Error  - StatusCode {response.StatusCode.GetHashCode()} {response.ReasonPhrase} {response.RequestMessage.RequestUri}";

            if (!response.IsSuccessStatusCode)
                throw new BadRequestException(errorMessage);

            try
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(errorMessage + " - Error al leer la respuesta - " + ex.Message);  
            }
        }
    }
}
