using IncidentPrioritization.Interfaces;
using IncidentPrioritization.Configs;
using IncidentPrioritization.DTO;
using Microsoft.Extensions.Options;
using IncidentPrioritization.Enums;

namespace IncidentPrioritization.Services
{
    public class ITSMService : IITSMService
    {
        private readonly IWebApi _webApi;
        private readonly IDataService _dataService;
        private readonly ITSMConfiguration _itsmConfiguration;

        //private const string ITSMUrl = "ITSM DB URL";
        public ITSMService(IWebApi webApi, IDataService dataService, IOptions<ITSMConfiguration> itsmConfiguration)
        {
            _webApi = webApi;
            _dataService = dataService;
            _itsmConfiguration = itsmConfiguration.Value;
        }

        public async Task<Response> ITSMRequest(string login, string requestId)
        {
            bool isIncExpired = await _dataService.CheckInc(requestId);
            if (isIncExpired == true)
            {
                string message = "Инцидент ускорен, так как нарушен крайний срок решения инцидента";
                await _dataService.WriteAsync(requestId, StatusCode.Success, message);
                return new Response() { StatusCode = StatusCode.Success, Message = message };

            }
            else
            {
                bool isUserExists = await _dataService.CheckUser(login);
                if (isUserExists == true)
                {
                    string message = "Инцидент ускорен, так как инициатор является привилегированным пользователем";
                    await _dataService.WriteAsync(requestId, StatusCode.Success, message);
                    return new Response() { StatusCode = StatusCode.Success, Message = message };
                }
                else
                {
                    string message = "Не соблюдены критерии";
                    await _dataService.WriteAsync(requestId, StatusCode.Error, message);
                    return new Response() { StatusCode = StatusCode.Error, Message = message };
                }
            }
        }

    }
}

