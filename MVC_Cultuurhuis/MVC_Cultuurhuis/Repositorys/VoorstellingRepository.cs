using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_Cultuurhuis.Models;

namespace MVC_Cultuurhuis.Repositorys
{
    public class VoorstellingRepository
    {
        private CultuurHuisMVCEntities db = new CultuurHuisMVCEntities();

        public List<Voorstelling> GetVoorstellingen()
        {
           return db.Voorstellingen.ToList();
        }

        public Voorstelling GetVoorstelling(int? id)
        {
            return db.Voorstellingen.Find(id);
        }
    }
}