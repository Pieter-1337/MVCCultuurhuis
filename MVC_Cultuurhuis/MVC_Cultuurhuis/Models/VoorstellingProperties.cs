using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_Cultuurhuis.Models
{
    public class VoorstellingProperties
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy HH:mm}")]
        public System.DateTime Datum { get; set; }
        [DisplayFormat(DataFormatString = "{0:€ #,##0.00}")]
        public decimal Prijs { get; set; }
        public short VrijePlaatsen { get; set; }
    }
}