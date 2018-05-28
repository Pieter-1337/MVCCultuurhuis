using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MVC_Cultuurhuis.Repositorys;

namespace MVC_Cultuurhuis.Custom_Validations
{
    public class UsernameUniek : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            if(value == null)
            {
                return true;
            }
            if(!(value is string))
            {
                return false;
            }
            else
            {
                Repositorys.CultuurhuisRepository db = new Repositorys.CultuurhuisRepository();
                return !db.BestaatKlant((string)value);
            }
        }
    }
}