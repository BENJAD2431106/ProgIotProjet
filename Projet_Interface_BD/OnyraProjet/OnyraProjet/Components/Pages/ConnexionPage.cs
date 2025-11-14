using Microsoft.AspNetCore.Components;
using OnyraProjet.Models;
using OnyraProjet.Services;
using System.Threading.Tasks;

namespace OnyraProjet.Pages
{
    public class ConnexionPage : ComponentBase
    {
        [Inject]
        public ConnexionService ConnexionService { get; set; }

        protected ConnexionModel infos = new();
        protected string message = "";

        protected async Task SeConnecter()
        {
            int resultat = await ConnexionService.UP_ConnexionUtilisateur(infos.AdresseCourriel, infos.MotDePasse);

            if (resultat == -1)
                message = "Courriel ou mot de passe incorrect !";
            else
                message = $"Connexion réussie !";
        }
    }
}
