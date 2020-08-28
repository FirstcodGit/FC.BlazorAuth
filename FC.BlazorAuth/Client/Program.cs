using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;

namespace FC.BlazorAuth.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(options => options.GetRequiredService<CustomAuthenticationStateProvider>());
            
            builder.RootComponents.Add<App>("app");
            builder.Services.AddBaseAddressHttpClient();

            await builder.Build().RunAsync();
        }
    }
}
