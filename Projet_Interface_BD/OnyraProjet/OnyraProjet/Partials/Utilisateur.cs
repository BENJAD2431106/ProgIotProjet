using System.ComponentModel.DataAnnotations.Schema;

namespace OnyraProjet.Models
{
    public partial class Utilisateur
    {
        [NotMapped]
        public string mdpInscription { get; set; } 
    }
}
