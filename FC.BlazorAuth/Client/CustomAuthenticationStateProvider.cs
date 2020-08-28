using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace FC.BlazorAuth.Client
{
    enum StorageType
    {
        token,
        expiry
    }

    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jSRuntime;

        public CustomAuthenticationStateProvider(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await GetTokenAsync();

            var identity = string.IsNullOrEmpty(token) ? new ClaimsIdentity() : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task SetTokenAsync(string token, string expiry)
        {
            if(token == null)
            {
                // press F12
                await _jSRuntime.InvokeAsync<object>("localStorage.removeItem", StorageType.token);
                await _jSRuntime.InvokeAsync<object>("localStorage.removeItem", StorageType.expiry);
            }
            else
            {
                await _jSRuntime.InvokeAsync<object>("localStorage.setItem", StorageType.token, token);
                await _jSRuntime.InvokeAsync<object>("localStorage.setItem", StorageType.expiry, expiry);
            }

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<string> GetTokenAsync()
        {
            var expiry = await _jSRuntime.InvokeAsync<object>("localStorage.getItem", StorageType.expiry);

            if(expiry != null)
            {
                if(DateTime.Parse(expiry.ToString()) > DateTime.Now.ToUniversalTime())
                {
                    return await _jSRuntime.InvokeAsync<string>("localStorage.getItem",StorageType.token);
                }
                else
                {
                    await SetTokenAsync(null, null);
                }
            }

            return null;
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            return Convert.FromBase64String(base64);
        }
    }
}
