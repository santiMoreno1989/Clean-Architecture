namespace CleanArchitecture.Application.Services
{
    public interface IHttpAlkemyService
    {
        Task<T> GetAlkemyResource<T>(string uri);
        Task<T> PostResource<T>(string url, T content);
    }
}