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
            var dbContext = myCalendarFactory.CreateDbContextAsync().Result;

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

    }
}


