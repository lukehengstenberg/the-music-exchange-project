using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TheMusicExchangeProject.Models
{
    /**
     * 
     * This Model structures the Users table in the DB.
     * 
     * */
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
        [PersonalData]
        public double Latitude { get; set; }
        [PersonalData]
        public double Longitude { get; set; }
        [PersonalData]
        public byte[] ProfilePicture { get; set; }
        public ICollection<Skill> Skills { get; set; }
    }
}
