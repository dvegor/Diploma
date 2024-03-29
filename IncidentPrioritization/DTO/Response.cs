using IncidentPrioritization.Enums;

namespace IncidentPrioritization.DTO
{
    public class Response
    {
        public StatusCode StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
