using Microsoft.EntityFrameworkCore;
using OnyraProjet.Data;
using OnyraProjet.Models;

namespace OnyraProjet.Services
{
    public class DonneeService
    {
        private readonly IDbContextFactory<Prog3a25ProductionAllysonJadContext> _factory;

        public DonneeService(IDbContextFactory<Prog3a25ProductionAllysonJadContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Donnee>> GetDonneesByUtilisateurAsync(int noUtilisateur)
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.Donnees
                .Where(d => d.NoUtilisateur == noUtilisateur)
                .OrderByDescending(d => d.DateHeure)
                .ToListAsync();
        }


    }
}
