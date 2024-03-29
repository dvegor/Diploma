using IncidentPrioritization.Enums;
using System.ComponentModel.DataAnnotations;

namespace IncidentPrioritization.Entities
{
    public class Incidents
    {
        [Key]
        public int Id { get; set; }
        public string? IncNumber { get; set; }
        public DateTime DateTime { get; set; }
        public StatusCode StatusCode { get; set; }
        public string? Description{ get; set; }
    }
}
