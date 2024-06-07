using Microsoft.AspNetCore.Authentication;
using ServerAPI.Handlers;
using ServerAPI.Services;

namespace ServerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddGrpc();

            builder.Services.AddScoped<IApiKeyAuthenticationService, ApiKeyAuthenticationService>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "ApiKey";
            }).AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", configureOptions => { });

            builder.Services.AddAuthorization();


            var app = builder.Build();


            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapGrpcService<MessageInventoryService>();


            app.Run();
        }
    }
}
