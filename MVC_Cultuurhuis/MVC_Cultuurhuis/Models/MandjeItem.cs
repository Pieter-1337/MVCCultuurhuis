using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_Cultuurhuis.Models
{
    public class MandjeItem
    {
        public int VoorstellingNr { get; set; }
        public string Titel { get; set; }
        public string Uitvoerders { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/YYYY HH:mm}")]
        public System.DateTime Datum { get; set; }

        [DisplayFormat(DataFormatString = "{0:€ #, ##0.00}")]
        public decimal Prijs { get; set; }

        public short Plaatsen { get; set; }

        public MandjeItem(int nr, string titel, string uitvoerders, DateTime datum, decimal prijs, short plaatsen)
        {
            this.VoorstellingNr = nr;
            this.Titel = titel;
            this.Uitvoerders = uitvoerders;
            this.Datum = datum;
            this.Prijs = prijs;
            this.Plaatsen = plaatsen;
            
        }
    }
}