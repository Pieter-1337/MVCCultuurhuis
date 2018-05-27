using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Cultuurhuis.Models;
using MVC_Cultuurhuis.Repositorys;

namespace MVC_Cultuurhuis.Controllers
{
    public class VoorstellingController : Controller
    {
        private VoorstellingRepository _voorstellingRepository = null;

        public VoorstellingController()
        {
            _voorstellingRepository = new VoorstellingRepository();

        }

        [HttpGet]
        // GET: Voorstelling
        public ActionResult Index(int? id)
        {
            var voorstelling =_voorstellingRepository.GetVoorstelling(id);
            return View(voorstelling);
        }

        [HttpPost]
        public ActionResult Reserveren(int? id)
        {
            var voorstelling = _voorstellingRepository.GetVoorstelling(id);
            var BesteldeTickets = Convert.ToInt32(Request["aantalPlaatsen"].ToString());
            if(BesteldeTickets > 0 && BesteldeTickets <= voorstelling.VrijePlaatsen)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Voorstelling", new { id = id });
            }
            


        }
    }
}