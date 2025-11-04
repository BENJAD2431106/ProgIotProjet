using Microsoft.AspNetCore.OutputCaching;
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

        //public async Task<?> UP_ConnexionUtilisateur(string AdresseCourriel, string MotDePasse)
        //{

        //}
        public void Seconnecter()
        {
            
        }
    }
}
