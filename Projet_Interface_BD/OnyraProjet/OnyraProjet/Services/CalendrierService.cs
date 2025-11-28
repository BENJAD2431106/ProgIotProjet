using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnyraProjet.Data;
using OnyraProjet.Models;
using OnyraProjet.Partials;

namespace OnyraProjet.Services
{
    public class CalendrierService
    {

        private readonly IDbContextFactory<Prog3a25ProductionAllysonJadContext> myCalendarFactory;

        public CalendrierService(IDbContextFactory<Prog3a25ProductionAllysonJadContext> myFactory)
        {
            this.myCalendarFactory = myFactory;
        }
        public async Task AjouterCalendrier(Calendrier calendrier)
        {
            var dbContext = await myCalendarFactory.CreateDbContextAsync();

            try
            {
                dbContext.Calendriers.Add(calendrier);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public async Task<string> SavoirSiRempli(DateTime date, int idUtilisateur)
        {
            using var db = await myCalendarFactory.CreateDbContextAsync();

            bool existe = await db.Calendriers
                .AnyAsync(c =>
                    c.NoUtilisateur == idUtilisateur &&
                    c.Dates == DateOnly.FromDateTime(date));

            if (existe)
                return "background-color: lightgreen;";

            return "";
        }


    }
}


