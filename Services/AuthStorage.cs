using Blazored.LocalStorage;
using Indego.Models;

namespace Indego.Services
{
    public interface IAuthStorage
    {
        ValueTask<UserSession?> LoadUserSession();
        ValueTask SaveUserSession(UserSession userSession);

        ValueTask DeleteUserSession();

    }
    public class AuthStorage : IAuthStorage
    {
        private const string localStorageKey = "session";

        private readonly ILocalStorageService _localStorage;

        public AuthStorage(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public ValueTask<UserSession?> LoadUserSession() => _localStorage.GetItemAsync<UserSession?>(localStorageKey);

        public ValueTask SaveUserSession(UserSession userSession) => _localStorage.SetItemAsync(localStorageKey, userSession);

        public ValueTask DeleteUserSession() => _localStorage.RemoveItemAsync(localStorageKey);
    }
}