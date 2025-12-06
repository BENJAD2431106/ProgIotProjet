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
            var param5 = new SqlParameter("@photoParam", System.Data.SqlDbType.VarBinary);

            // 1. Gérer le cas où la photo est NULL (pas d'image)
            if (nouvelUtilisateur.Photo == null)
            {
                // C'est crucial : L'utilisation de DBNull.Value garantit que SQL Server interprète cela comme NULL.
                param5.Value = DBNull.Value;
            }
            else
            {
                // 2. Si la photo existe, passez le tableau de bytes
                param5.Value = nouvelUtilisateur.Photo;
            }

            param5.IsNullable = true; // Déjà présent, mais à confirmer (pas strictement nécessaire si Value = DBNull.Value)
            param5.Size = -1;
            var param6 = new SqlParameter("@ramQParam", nouvelUtilisateur.RamQ);
            var param7 = new SqlParameter("@ageParam", nouvelUtilisateur.Age);
            var outPut = new SqlParameter("@no", System.Data.SqlDbType.Int);
            outPut.Direction = System.Data.ParameterDirection.Output;

            dbContext.Database.ExecuteSqlRaw("EXECUTE UP_InscrireUtilisateur @courrielParam, @nomParam, @prenomParam, @mdpParam, @photoParam, @ramQParam, @ageParam, @no OUTPUT ", param1, param2, param3, param4, param5, param6, param7, outPut);
            UserSession2 user = new UserSession2();
            try
            {
                user.Name = nouvelUtilisateur.PrenomUtilisateur + " " + nouvelUtilisateur.NomUtilisateur;
                user.Id = (int)outPut.Value;
                user.Role = "User";
                await auth.UpDateAuthenticationState(user);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

        }
        public async Task<bool> UtilisateurExiste(string courriel)
        {
            var db = await myFactory.CreateDbContextAsync();

            return await db.Utilisateurs.AnyAsync(u => u.Courriel == courriel);
        }
    }
}
