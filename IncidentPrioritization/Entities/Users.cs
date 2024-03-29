using System.ComponentModel.DataAnnotations;

namespace IncidentPrioritization.Entities
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string? Login { get; set; }
    }
}

