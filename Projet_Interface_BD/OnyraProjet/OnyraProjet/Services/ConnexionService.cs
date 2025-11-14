using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.VisualBasic;
using OnyraProjet.Data;
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

        public async Task<int> UP_ConnexionUtilisateur(string adresseCourriel, string motDePasse)
        {
            using var context = await _factory.CreateDbContextAsync();

            var paramCourriel = new SqlParameter("@courriel", adresseCourriel);
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

            return (int)paramNoUtilisateur.Value;
        }
        public void Seconnecter()
        {
            
        }
    }
}
