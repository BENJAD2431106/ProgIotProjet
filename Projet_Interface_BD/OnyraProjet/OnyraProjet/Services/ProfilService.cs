//using Microsoft.EntityFrameworkCore;
//using OnyraProjet.Data;
//using OnyraProjet.Models;

//namespace OnyraProjet.Services
//{
//    public class ProfilService
//    {
//        private readonly Prog3a25ProductionAllysonJadContext _context;

//        public ProfilService(Prog3a25ProductionAllysonJadContext context)
//        {
//            _context = context;
//        }

//        public async Task<ProfilModel?> GetProfilAsync(int noUtilisateur)
//        {
//            var user = await _context.Utilisateurs.FindAsync(noUtilisateur);
//            if (user == null) return null;

//            return new ProfilModel
//            {
//                NomUtilisateur = user.NomUtilisateur,
//                PrenomUtilisateur = user.PrenomUtilisateur,
//                Courriel = user.Courriel,
//                Age = user.Age,
//                RamQ = user.RamQ,
//                Photo = user.Photo
//            };
//        }

//        public async Task<bool> UpdateProfilAsync(int noUtilisateur, ProfilModel model)
//        {
//            var user = await _context.Utilisateurs.FindAsync(noUtilisateur);
//            if (user == null) return false;

//            user.NomUtilisateur = model.NomUtilisateur;
//            user.PrenomUtilisateur = model.PrenomUtilisateur;
//            user.Courriel = model.Courriel;
//            user.Age = model.Age;
//            user.RamQ = model.RamQ;
//            if (model.Photo != null)
//                user.Photo = model.Photo;

//            await _context.SaveChangesAsync();
//            return true;
//        }
//    }
//}

