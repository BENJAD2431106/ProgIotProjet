using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnyraProjet.Data;
using OnyraProjet.Models;
using OnyraProjet.Partials;

namespace OnyraProjet.Services
{
    public class InscriptionService
    {
        private readonly IDbContextFactory<Prog3a25ProductionAllysonJadContext> myFactory;

        public InscriptionService(IDbContextFactory<Prog3a25ProductionAllysonJadContext> myFactory)
        {
            this.myFactory = myFactory;
        }
        public async Task AjouterUtilisateur_UP(Utilisateur nouvelUtilisateur)
        {
            var dbContext = myFactory.CreateDbContextAsync().Result;
            var param1 = new SqlParameter("courrielParam", nouvelUtilisateur.Courriel);
            var param2 = new SqlParameter("nomParam", nouvelUtilisateur.NomUtilisateur);
            var param3 = new SqlParameter("prenomParam", nouvelUtilisateur.PrenomUtilisateur);
            var param4 = new SqlParameter("mdpParam", nouvelUtilisateur.MotDePasse);
            var param5 = new SqlParameter("photoParam", nouvelUtilisateur.Photo);
            var param6 = new SqlParameter("nasParam", nouvelUtilisateur.AssuranceSociale);
            //ramQ ici pas nas.
            var param7 = new SqlParameter("ageParam", nouvelUtilisateur.Age);

            await dbContext.Database.ExecuteSqlRawAsync("EXECUTE UP_InscrireUtilisateur @courrielParam, @nomParam, @prenomParam, @mdpParam, @photoParam, @nasParam, @ageParam", param1, param2, param3, param4, param5, param6, param7);
        }

    }
}
