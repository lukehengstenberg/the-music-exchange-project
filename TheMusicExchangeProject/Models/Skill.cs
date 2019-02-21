using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheMusicExchangeProject.Areas.Identity.Data;

namespace TheMusicExchangeProject.Models
{
    public class Skill
    {
        public int SkillID { get; set; }
        public string SkillName { get; set; }
        public string UserID { get; set; }
        public int LevelID { get; set; }
        public string Description { get; set; }

        public TheMusicExchangeProjectUser User { get; set; }
        public SkillLevel Level { get; set; }
    }
}
