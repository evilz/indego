using System.Text;
using Blazored.LocalStorage;
using Indego.Client;
using Indego.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Indego.Services
{

    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
    }
    public class AuthService : IAuthService
    {
        private readonly IIndegoClient _indegoClient;
        private readonly ApiAuthenticationStateProvider _authenticationStateProvider;
        private readonly IAuthStorage _authStorage;
        public AuthService(IIndegoClient indegoClient, AuthenticationStateProvider authenticationStateProvider, IAuthStorage authStorage)
        {
            _indegoClient = indegoClient;
            _authenticationStateProvider = (ApiAuthenticationStateProvider)authenticationStateProvider;
            _authStorage = authStorage;
        }



        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var basicAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{loginModel.Email}:{loginModel.Password}"));
            Console.WriteLine(basicAuth);
            var response = await _indegoClient.AuthenticateAsync(AuthenticateRequest.Default, $"Basic {basicAuth}");

            if (!response.IsSuccessStatusCode)
            {
                return new LoginResult(false, "Invalid credentials", null);
            }

            if (response.Content == null)
            {
                return new LoginResult(false, "Invalid response", null);
            }


            var auth = response.Content;
            var userSession = new UserSession(loginModel.Email, auth.ContextId, auth.UserId, auth.Alm_sn);
            await _authStorage.SaveUserSession(userSession);
            _authenticationStateProvider.MarkUserAsAuthenticated();

            return new LoginResult(true, null, response.Content.ContextId);
        }

        public async Task Logout()
        {
            await _authStorage.DeleteUserSession();
            _authenticationStateProvider.MarkUserAsLoggedOut();
        }
    }
}