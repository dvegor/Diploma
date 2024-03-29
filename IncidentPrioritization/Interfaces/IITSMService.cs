using IncidentPrioritization.DTO;

namespace IncidentPrioritization.Interfaces
{
    public interface IITSMService
    {
        Task<Response> ITSMRequest(string login, string requestId);
    }
}
