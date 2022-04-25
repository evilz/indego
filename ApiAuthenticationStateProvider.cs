using System.Security.Claims;
using Blazored.LocalStorage;
using Indego.Models;
using Indego.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Indego;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly IAuthStorage _authStorage;

    public ApiAuthenticationStateProvider(HttpClient httpClient, IAuthStorage authStorage)
    {
        _httpClient = httpClient;
        _authStorage = authStorage;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var userSession = await _authStorage.LoadUserSession();
        if (userSession == null)
        {
            Console.WriteLine("No auth");
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        _httpClient.DefaultRequestHeaders.Add("x-im-context-id", userSession.ContextId.ToString());

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.PrimarySid, userSession.ContextId.ToString()) ,
            new Claim(ClaimTypes.Name, userSession.Email),
            new Claim(ClaimTypes.SerialNumber, userSession.SerialNumber)
            }, "apiauth")));
    }

    public void MarkUserAsAuthenticated()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void MarkUserAsLoggedOut()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

}
