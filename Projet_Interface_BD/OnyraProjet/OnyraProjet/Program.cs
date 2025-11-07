using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using OnyraProjet.Authentication;
using OnyraProjet.Components;
using OnyraProjet.Data;
using OnyraProjet.Services;

namespace OnyraProjet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var conStrBuilder = new SqlConnectionStringBuilder(
                builder.Configuration.GetConnectionString("ConnexionDBDev"));
            conStrBuilder.Password = builder.Configuration["Password"];

            builder.Services.AddPooledDbContextFactory<Prog3a25ProductionAllysonJadContext>(
                x=>x.UseSqlServer(conStrBuilder.ConnectionString));

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddScoped<ConnexionService>();
            builder.Services.AddScoped<WeatherService>();
            builder.Services.AddScoped<ProtectedSessionStorage>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthentificationStateProvider>();
            builder.Services.AddScoped<AddAuthenticationCore>();

            //mine
            builder.Services.AddScoped<InscriptionService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
