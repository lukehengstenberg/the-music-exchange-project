using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMusicExchangeProject.Models
{
    public class UserSkillsViewModel
    {
        public TheMusicExchangeProjectUser Users { get; set; }
        public Skill Skills { get; set; }
        public string SkillLevel { get; set; }
        public int Age { get; set; }
        public double? Distance { get; set; }
        //public IList<Connection> Connections { get; set; }
        //public IList<Block> Blocks { get; set; }
    }
}
