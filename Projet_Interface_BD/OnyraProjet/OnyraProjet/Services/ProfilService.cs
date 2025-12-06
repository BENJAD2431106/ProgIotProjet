using Microsoft.EntityFrameworkCore;
using OnyraProjet.Data;
using OnyraProjet.Models;

namespace OnyraProjet.Services
{
    public class ProfilService
    {
        private readonly IDbContextFactory<Prog3a25ProductionAllysonJadContext> _contextFactory;

        public ProfilService(IDbContextFactory<Prog3a25ProductionAllysonJadContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<ProfilModel?> GetProfilAsync(int noUtilisateur)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            var user = await context.Utilisateurs
                                    .FirstOrDefaultAsync(u => u.NoUtilisateur == noUtilisateur);

            if (user == null)
                return null;

            return new ProfilModel
            {
                NomUtilisateur = user.NomUtilisateur,
                PrenomUtilisateur = user.PrenomUtilisateur,
                Courriel = user.Courriel,
                Age = user.Age,
                RamQ = user.RamQ,
                Photo = user.Photo
            };
        }

        public async Task<bool> UpdateProfilAsync(int noUtilisateur, ProfilModel model)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            var user = await context.Utilisateurs
                                    .FirstOrDefaultAsync(u => u.NoUtilisateur == noUtilisateur);

            if (user == null)
                return false;

            user.NomUtilisateur = model.NomUtilisateur;
            user.PrenomUtilisateur = model.PrenomUtilisateur;
            user.Courriel = model.Courriel;
            user.Age = model.Age;
            user.RamQ = model.RamQ;

            if (model.Photo != null && model.Photo.Length > 0)
            {
                user.Photo = model.Photo;
            }

            await context.SaveChangesAsync();
            return true;
        }
    }
}
