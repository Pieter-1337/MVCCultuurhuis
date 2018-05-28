using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MVC_Cultuurhuis.Custom_Validations;

namespace MVC_Cultuurhuis.Models
{
    public class NieuweKlantViewModel
    {
        [Required(ErrorMessage = "Voornaam is verplicht in te vullen")]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "Familienaam is verplicht in te vullen")]
        public string Familienaam { get; set; }

        [Required(ErrorMessage = "Straat is verplicht in te vullen")]
        public string Straat { get; set; }

        [Required(ErrorMessage = "Huisnr is verplicht in te vullen")]
        public string Huisnr { get; set; }

        [Required(ErrorMessage = "Postcode is verplicht in te vullen")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Gemeente is verplicht in te vullen")]
        public string Gemeente { get; set; }

        [Required(ErrorMessage = "Gebruikersnaam is verplicht in te vullen")]
        [UsernameUniek(ErrorMessage = "Deze gebruikersnaam is reeds in gebruik gelieve een andere te kiezen")]
        public string Gebruikersnaam { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht in te vullen")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Paswoord { get; set; }

        [Required(ErrorMessage = "Wachtwoord moet bevestigd worden")]
        [DataType(DataType.Password)]
        [Compare("Paswoord", ErrorMessage = "De ingevulde paswoorden zijn niet dezelfde")]
        [Display(Name = "Wachtwoord bevestigen")]
        public string HerhaalPaswoord { get; set; }
    }
}