using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnyraProjet.Authentication;
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
        public async Task AjouterUtilisateur_UP(Utilisateur nouvelUtilisateur, CustomAuthentificationStateProvider auth)
        {
            var dbContext = await myFactory.CreateDbContextAsync();

            var param1 = new SqlParameter("@courrielParam", nouvelUtilisateur.Courriel);
            var param2 = new SqlParameter("@nomParam", nouvelUtilisateur.NomUtilisateur);
            var param3 = new SqlParameter("@prenomParam", nouvelUtilisateur.PrenomUtilisateur);
            var param4 = new SqlParameter("@mdpParam", nouvelUtilisateur.mdpInscription);
            var param5 = new SqlParameter("@photoParam", nouvelUtilisateur.Photo);
            param5.IsNullable = true;
            param5.DbType = System.Data.DbType.Binary;  
            var param6 = new SqlParameter("@ramQParam", nouvelUtilisateur.RamQ);
            var param7 = new SqlParameter("@ageParam", nouvelUtilisateur.Age);
            var outPut = new SqlParameter("@no", System.Data.SqlDbType.Int);
            outPut.Direction=System.Data.ParameterDirection.Output;
         
            dbContext.Database.ExecuteSqlRaw("EXECUTE UP_InscrireUtilisateur @courrielParam, @nomParam, @prenomParam, @mdpParam, NULL, @ramQParam, @ageParam, @no OUTPUT ", param1, param2, param3, param4, param6, param7, outPut);
            //UserSession user = new UserSession(nouvelUtilisateur, "User");
            UserSession2 user = new UserSession2();
            user.Name = nouvelUtilisateur.PrenomUtilisateur + " " + nouvelUtilisateur.NomUtilisateur;
            user.Id = (int)outPut.Value;
            user.Role = "User";
            await auth.UpDateAuthenticationState(user);
        }

    }
}
