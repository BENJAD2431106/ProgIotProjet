using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using OnyraProjet.Components;
using OnyraProjet.Data;

namespace OnyraProjet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var conStrBuilder = new SqlConnectionStringBuilder(
                builder.Configuration.GetConnectionString("ConnexionDB"));
            conStrBuilder.Password = builder.Configuration["Password"];

            builder.Services.AddPooledDbContextFactory<Prog3a25ProductionAllysonJadContext>(
                x=>x.UseSqlServer(conStrBuilder.ConnectionString));

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

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
