using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnyraProjet.Data;
using System.Data;

namespace OnyraProjet.Services
{
    public class MotDePasseService
    {
        private readonly IDbContextFactory<Prog3a25ProductionAllysonJadContext> _factory;

        public MotDePasseService(IDbContextFactory<Prog3a25ProductionAllysonJadContext> factory)
        {
            _factory = factory;
        }

        // Récupérer le courriel selon l'ID utilisateur
        public async Task<string?> GetCourrielAsync(int userId)
        {
            await using var context = await _factory.CreateDbContextAsync();

            return await context.Utilisateurs
                                .Where(u => u.NoUtilisateur == userId)
                                .Select(u => u.Courriel)
                                .FirstOrDefaultAsync();
        }

        public async Task<bool> ChangerMotDePasseAsync(string courriel, string motDePasseActuel, string nouveauMotDePasse)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var connection = (SqlConnection)context.Database.GetDbConnection();
            await connection.OpenAsync();

            int noUtiliateur;

            // ---- APPEL DE LA PROCÉDURE STOCKÉE ----
            using (var cmd = new SqlCommand("UP_ConnexionUtilisateur", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@courriel", SqlDbType.NVarChar, 255)
                {
                    Value = courriel
                });

                cmd.Parameters.Add(new SqlParameter("@motDePasse", SqlDbType.NVarChar, 255)
                {
                    Value = motDePasseActuel
                });

                // LE NOM EXACT QUI FIGURE DANS LA PROC SQL (AVEC LA FAUTE)
                var outputParam = new SqlParameter("@noUtiliateur", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                await cmd.ExecuteNonQueryAsync();

                noUtiliateur = outputParam.Value is int i ? i : -1;
            }

            // Si login impossible → retour fail
            if (noUtiliateur <= 0)
                return false;

            // ---- MISE À JOUR DU MOT DE PASSE ----
            const string updateSql = @"
            UPDATE Utilisateurs
            SET MotDePasse = HASHBYTES('SHA2_512', @NewPwd + CAST(sel AS NVARCHAR(36)))
            WHERE NoUtilisateur = @noUtiliateur";

            using (var cmdUpdate = new SqlCommand(updateSql, connection))
            {
                cmdUpdate.Parameters.Add(new SqlParameter("@NewPwd", SqlDbType.NVarChar, 255)
                {
                    Value = nouveauMotDePasse
                });

                cmdUpdate.Parameters.Add(new SqlParameter("@noUtiliateur", SqlDbType.Int)
                {
                    Value = noUtiliateur
                });

                var rows = await cmdUpdate.ExecuteNonQueryAsync();
                return rows == 1;
            }
        }
    }
}
