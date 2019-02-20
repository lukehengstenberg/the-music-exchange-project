using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TheMusicExchangeProject.Areas.Identity.Data
{
    public class TheMusicExchangeProjectUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
        [PersonalData]
        public string Bio { get; set; }
        [PersonalData]
        public string Postcode { get; set; }
    }
}
