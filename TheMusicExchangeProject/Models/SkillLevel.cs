using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheMusicExchangeProject.Models
{
    /**
     * 
     * This Model structures the SkillLevels table in the DB.
     * 
     * */
    public class SkillLevel
    {
        [Key]
        public int LevelID { get; set; }

        public string Name { get; set; }

        public ICollection<Skill> Skills { get; set; }
    }
}
