namespace IncidentPrioritization.Interfaces
{
    public interface IWebApi
    {
        Task<T> GetAsync<T>(string api, string HeaderAccept = "application/json");
        Task<U> PostAsync<T, U>(string api, T obj);
        Task<string> PostAsync<T>(string api, T obj);
        Task DeleteAsync(string api);

    }
}