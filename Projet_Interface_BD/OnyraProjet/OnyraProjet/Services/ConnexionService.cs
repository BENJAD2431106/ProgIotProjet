using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.VisualBasic;
using OnyraProjet.Authentication;
using OnyraProjet.Data;
using OnyraProjet.Models;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OnyraProjet.Services
{
    public class ConnexionService
    {
        private IDbContextFactory<Prog3a25ProductionAllysonJadContext> _factory;
        public ConnexionService(IDbContextFactory<Prog3a25ProductionAllysonJadContext> factory)
        {
            _factory = factory;
        }

        public async Task<UserSession2> ConnexionEtRecupererSession(string courriel, string motDePasse)
        {
            using var context = await _factory.CreateDbContextAsync(); /*using = garantit que le contexte sera détruit après l’opération*/

            var paramCourriel = new SqlParameter("@courriel", courriel);
            var paramMotDePasse = new SqlParameter("@motDePasse", motDePasse);

            var paramNoUtilisateur = new SqlParameter
            {
                ParameterName = "@noUtilisateur",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            await context.Database.ExecuteSqlRawAsync(
                "EXEC UP_ConnexionUtilisateur @courriel, @motDePasse, @noUtilisateur OUTPUT",
                paramCourriel, paramMotDePasse, paramNoUtilisateur);

            int id = (int)paramNoUtilisateur.Value; /*convertit la valeur retournée par SQL en un entier C#*/

            if (id == -1)
                return null;

            // Va récupérer l'utilisateur dans la BD
            var user = await context.Utilisateurs
                .Where(u => u.NoUtilisateur == id)
                .Select(u => new UserSession2 /*Construction de l'objet UserSession2*/
                {
                    Id = u.NoUtilisateur,
                    Name = u.PrenomUtilisateur + " " + u.NomUtilisateur,
                    Role = "User"
                })
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
