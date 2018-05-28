using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_Cultuurhuis.Models;

namespace MVC_Cultuurhuis.Repositorys
{
    public class CultuurhuisRepository
    {
        private CultuurHuisMVCEntities db = new CultuurHuisMVCEntities();

        public List<Genre> GetGenres()
        {
            return db.Genres.OrderBy(g => g.GenreNaam).ToList();
        }

        public Genre GetGenre(int? id)
        {
            return db.Genres.Find(id);
        }

        public List<Voorstelling> GetVoorstellingenOpGenre(int? id)
        {
            var query = from voorstelling in db.Voorstellingen.Include("Genre")
                        where voorstelling.Datum <= DateTime.Today && voorstelling.GenreNr == id
                        orderby voorstelling.Datum select voorstelling;
            return query.ToList();
        }

        public Klant GetKlant(string naam, string paswoord)
        {
            var Klant = (from klant in db.Klanten where klant.GebruikersNaam == naam && klant.Paswoord == paswoord select klant).FirstOrDefault();
            return Klant;
        }

        public bool BestaatKlant(string gebruikersnaam)
        {
            var bestaandeKlant = (from klant in db.Klanten where klant.GebruikersNaam == gebruikersnaam select klant).FirstOrDefault();
            return bestaandeKlant != null;
        }

        public void VoegKlantToe(Klant klant)
        {
            db.Klanten.Add(klant);
            db.SaveChanges();
        }
    
    }
}