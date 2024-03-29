using IncidentPrioritization.DTO;
using IncidentPrioritization.Enums;

namespace IncidentPrioritization.Interfaces
{
    public interface IDataService
    {
        Task<bool> CheckUser(string login);
        Task<bool> CheckInc(string requestId);
        Task WriteAsync(string requestId, StatusCode statusCode, string message);
    }
}
