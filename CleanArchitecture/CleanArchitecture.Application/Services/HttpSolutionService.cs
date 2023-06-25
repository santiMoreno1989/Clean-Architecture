using CleanArchitecture.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace CleanArchitecture.Application.Services
{
    public class HttpSolutionService
    {
        private readonly HttpClient _httpClient;

        public HttpSolutionService(HttpClient httpclient)
        {
            _httpClient = httpclient;
        }

        public async Task<T> GetSolutionResource<T>(string uri, object values = null)
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
    }
}
