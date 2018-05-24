using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Cultuurhuis.Repositorys;
using MVC_Cultuurhuis.Models;

namespace MVC_Cultuurhuis.Controllers
{
    public class HomeController : Controller
    {
        private GenreRepository _genreRepository = null;
        private CultuurhuisRepository _cultuurhuisRepository = null;
        
        

        public HomeController()
        {
            _genreRepository = new GenreRepository();
            _cultuurhuisRepository = new CultuurhuisRepository();

        }

        public ActionResult Index(int? id)
        {
            var VoorstellingenGenreModel = new VoorstellingenGenreModel();
            VoorstellingenGenreModel.Genres = _cultuurhuisRepository.GetGenres();
            VoorstellingenGenreModel.Genre = _cultuurhuisRepository.GetGenre(id);
            VoorstellingenGenreModel.Voorstellingen = _cultuurhuisRepository.GetVoorstellingenOpGenre(id);

            return View(VoorstellingenGenreModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}