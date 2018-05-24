using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_Cultuurhuis.Models;

namespace MVC_Cultuurhuis.Repositorys
{
    public class GenreRepository
    {
        private CultuurHuisMVCEntities db = new CultuurHuisMVCEntities();

        public List<Genre> GetGenres()
        {
            return db.Genres.ToList();
        }
    }
}