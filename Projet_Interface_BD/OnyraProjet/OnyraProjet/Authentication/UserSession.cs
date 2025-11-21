using OnyraProjet.Models;

namespace OnyraProjet.Authentication
{
    public class UserSession : Utilisateur
    {
        public string Role { get; set; }
    }
}
