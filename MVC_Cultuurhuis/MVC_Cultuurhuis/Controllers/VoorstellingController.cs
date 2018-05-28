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
        private CultuurhuisRepository _cultuurhuisRepository = null;

        public VoorstellingController()
        {
            _voorstellingRepository = new VoorstellingRepository();
            _cultuurhuisRepository = new CultuurhuisRepository();

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
                Session[id.ToString()] = BesteldeTickets;
                return RedirectToAction("Mandje", "Voorstelling");
            }
            else
            {
                return RedirectToAction("Index", "Voorstelling", new { id = id });
            }
        }

        public ActionResult Mandje()
        {
            decimal teBetalen = 0;
            List<MandjeItem> mandjeItems = new List<MandjeItem>();
            if (Session.Keys.Count != 0)
            {
                foreach (string nummer in Session)
                {
                    int voorstellingnummer;
                    if (int.TryParse(nummer, out voorstellingnummer))
                    {
                        Voorstelling voorstelling = _voorstellingRepository.GetVoorstelling(voorstellingnummer);
                        if (voorstelling != null)
                        {
                            MandjeItem mandjeItem = new MandjeItem(voorstelling.VoorstellingsNr, voorstelling.Titel, voorstelling.Uitvoerders, voorstelling.Datum, voorstelling.Prijs, Convert.ToInt16(Session[nummer]));
                            teBetalen += (mandjeItem.Plaatsen * mandjeItem.Prijs);
                            mandjeItems.Add(mandjeItem);
                        }
                    }
                }
                ViewBag.TeBetalen = teBetalen;
                return View(mandjeItems);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Verwijderen()
        {
            foreach(var item in Request.Form.AllKeys)
            {
                if(Session[item] != null)
                {
                    Session.Remove(item);
                }      
            }

            return RedirectToAction("Mandje", "Voorstelling");
        }

        public ActionResult Bevestiging(string naam, string paswoord)
        {
            if (Request["zoek"] != null)
            {
                var klant = _cultuurhuisRepository.GetKlant(naam, paswoord);
                if (klant != null)
                {
                    Session["Klant"] = klant;
                    return View();
                }
                else
                {
                    ViewBag.ErrorMessage = "de ingegeven gebruikersnaam of wachtwoord waren niet correct";
                    return View();
                }
                
            }
            if (Request["nieuw"] != null)
            {
                return RedirectToAction("NieuweKlant", "Voorstelling");
            }

            if (Request["bevestig"] != null)
            {
                //verwerking klantgegevens via Session["Klant"] 
                var klant = (Klant)Session["klant"];
                Session.Remove("klant");

                List<MandjeItem> gelukteReservaties = new List<MandjeItem>();
                List<MandjeItem> mislukteReservaties = new List<MandjeItem>();

                //haal alle reservaties uit de session 
                foreach (string nummer in Session)
                {
                    Reservatie nieuweReservatie = new Reservatie();
                    nieuweReservatie.VoorstellingsNr = int.Parse(nummer);
                    nieuweReservatie.Plaatsen = Convert.ToInt16(Session[nummer]);
                    nieuweReservatie.KlantNr = klant.KlantNr;

                    Voorstelling voorstelling = _voorstellingRepository.GetVoorstelling(nieuweReservatie.VoorstellingsNr);

                    if (voorstelling.VrijePlaatsen >= nieuweReservatie.Plaatsen)
                    {
                        //opslaan in database 
                        _voorstellingRepository.BewaarReservatie(nieuweReservatie);
                        gelukteReservaties.Add(new MandjeItem(voorstelling.VoorstellingsNr, voorstelling.Titel, voorstelling.Uitvoerders, voorstelling.Datum, voorstelling.Prijs, nieuweReservatie.Plaatsen));

                    }
                    else
                    {
                        mislukteReservaties.Add(new MandjeItem(voorstelling.VoorstellingsNr, voorstelling.Titel, voorstelling.Uitvoerders, voorstelling.Datum, voorstelling.Prijs, nieuweReservatie.Plaatsen));
                    }
                }
                Session.RemoveAll();
                Session["gelukt"] = gelukteReservaties;
                Session["mislukt"] = mislukteReservaties;
                return RedirectToAction("Overzicht", "Voorstelling");
            }
            return View();
        }

        [HttpGet]
        public ActionResult NieuweKlant()
        {
            var nieuwViewModel = new NieuweKlantViewModel();
            return View(nieuwViewModel);
        }

        [HttpPost]
        public ActionResult NieuweKlant(NieuweKlantViewModel form)
        {
            if (this.ModelState.IsValid)
            {
                Klant NieuweKlant = new Klant()
                {
                    Voornaam = form.Voornaam,
                    Familienaam = form.Familienaam,
                    Straat = form.Straat,
                    HuisNr = form.Huisnr,
                    Postcode = form.Postcode,
                    Gemeente = form.Gemeente,
                    GebruikersNaam = form.Gebruikersnaam,
                    Paswoord = form.Paswoord
                };

                Session["Klant"] = NieuweKlant;
                _cultuurhuisRepository.VoegKlantToe(NieuweKlant);
                return RedirectToAction("Bevestiging", "Voorstelling");
            }
            else
            {
                return View(form);
            }
        }

        public ActionResult Overzicht()
        {
            List<MandjeItem> gelukteReservaties = (List<MandjeItem>)Session["gelukt"];
            List<MandjeItem> mislukteReservaties = (List<MandjeItem>)Session["mislukt"];
            ViewBag.gelukt = gelukteReservaties;
            ViewBag.mislukt = mislukteReservaties;
            Session.Clear();
            return View();
        }
    }
}