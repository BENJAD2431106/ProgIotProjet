using Microsoft.EntityFrameworkCore;
using OnyraProjet.Data;
using OnyraProjet.Models;

namespace OnyraProjet.Services
{
    public class DonneesService
    {
        private readonly IDbContextFactory<Prog3a25ProductionAllysonJadContext> _factory;

        public DonneesService(IDbContextFactory<Prog3a25ProductionAllysonJadContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Donnees>> GetDonneesByUtilisateurAsync(int noUtilisateur)
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.DonneesCalendriers
                .Where(d => d.NoUtilisateur == noUtilisateur)
                .OrderByDescending(d => d.Dates)
                .ToListAsync();
        }


    }
}
