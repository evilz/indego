using System.Text;
using Indego.Client;

namespace Indego.Services
{



    public interface IIndegoService
    {
        Task<StatusResponse> GetStatusAsync();
        Task<string> GetMapAsync();
        Task<Alert[]> GetAlertsAsync();
    }
    public class IndegoService : IIndegoService
    {
        private readonly IIndegoClient _client;
        private readonly IAuthStorage authStorage;

        public IndegoService(IIndegoClient client, IAuthStorage authStorage)
        {
            this.authStorage = authStorage;
            _client = client;
        }
        public async Task<StatusResponse> GetStatusAsync()
        {
            var userSession = await authStorage.LoadUserSession();

            var response = await _client.GetStatusAsync(userSession.SerialNumber, userSession.ContextId); //, this.ContextId);

            if (response.IsSuccessStatusCode)
            {
                return response.Content;
            }
            else
            {
                throw new System.Exception("Error getting state");
            }

        }

        public async Task<string> GetMapAsync()
        {
            var userSession = await authStorage.LoadUserSession();

            var response = await _client.GetMapAsync(userSession.SerialNumber, userSession.ContextId); //, this.ContextId);

            if (response.IsSuccessStatusCode)
            {
                return response.Content;
            }
            else
            {
                throw new System.Exception("Error getting state");
            }

        }

        public async Task<Alert[]> GetAlertsAsync()
        {
            var userSession = await authStorage.LoadUserSession();

            var response = await _client.GetAlertsAsync(userSession.ContextId); //, this.ContextId);

            if (response.IsSuccessStatusCode)
            {
                return response.Content;
            }
            else
            {
                throw new System.Exception("Error getting alerts");
            }

        }
    }
}