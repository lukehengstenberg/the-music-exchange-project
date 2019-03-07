using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMusicExchangeProject.Areas.Identity.Data;

namespace TheMusicExchangeProject.Models
{
    public class UserSkillsViewModel
    {
        public TheMusicExchangeProjectUser Users { get; set; }
        public Skill Skills { get; set; }
        public string SkillLevel { get; set; }
        public int Age { get; set; }
    }
}
