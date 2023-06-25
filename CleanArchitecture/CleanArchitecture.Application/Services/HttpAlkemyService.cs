using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain;
using System.Net;
using System.Text;
using System.Text.Json;

namespace CleanArchitecture.Application.Services
{
    public class HttpAlkemyService : IHttpAlkemyService
    {
        private readonly HttpClient _httpClient;
        public HttpAlkemyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAlkemyResource<T>(string uri)
        {
            HttpResponseMessage responseMessage = new();
            try
            {
                responseMessage = await _httpClient.GetAsync(uri);
                responseMessage.EnsureSuccessStatusCode();
                var resultadoStr = await responseMessage.Content.ReadAsStringAsync();
                return DeserializeResponse<T>(resultadoStr);

            }
            catch (Exception)
            {


                if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    var cuerpo = await responseMessage.Content.ReadAsStringAsync();
                    var camposConErrores = Utilidades.ExtraerErroresWebApi(cuerpo);
                    var error = camposConErrores.Values.First();
                    throw new BadRequestException(error.First());

                }


                if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                    throw new KeyNotFoundException();

                throw;
            }
        }

        public async Task<T> PostResource<T>(string url, T content)
        {
            HttpResponseMessage responseMessage = new();

            try
            {
                var serialized = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
                responseMessage = await _httpClient.PostAsync(url, serialized);
                string responseBody = await responseMessage.Content.ReadAsStringAsync();

                return DeserializeResponse<T>(responseBody);
            }
            catch (Exception)
            {
                if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    var cuerpo = await responseMessage.Content.ReadAsStringAsync();
                    var camposConErrores = Utilidades.ExtraerErroresWebApi(cuerpo);
                    var error = camposConErrores.Values.FirstOrDefault();
                    var campo = camposConErrores.FirstOrDefault().Key;
                    throw new BadRequestException($"{campo} : {error}");

                }


                if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                    throw new KeyNotFoundException();

                throw;
            }
        }

        private static T DeserializeResponse<T>(string response)
            => JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}
