using IncidentPrioritization.DTO;
using IncidentPrioritization.Enums;
using IncidentPrioritization.Interfaces;
using IncidentPrioritization.Models;
using Microsoft.EntityFrameworkCore;

namespace IncidentPrioritization.Services
{
    public class DataService : IDataService
    {
        private readonly ITSMContext _ITSMContext;

        public DataService(ITSMContext ITSMContext) => _ITSMContext = ITSMContext;

        public async Task<bool> CheckUser(string login)
        {
            return await _ITSMContext.Users.AnyAsync(x => x.Login == login);
        }

        public async Task<bool> CheckInc(string requestId)
        {
            return await _ITSMContext.Incidents.AnyAsync(x => x.IncNumber == requestId && x.DateTime < DateTime.Now);
        }

        public async Task WriteAsync(string incId, StatusCode statusCode, string message)
        {
            var incident = await _ITSMContext.Incidents.FirstOrDefaultAsync(i => i.IncNumber == incId);
            if (incident == null) return;

            incident.StatusCode = statusCode;
            incident.Description = message;

            await _ITSMContext.SaveChangesAsync();
        }

    }
}
