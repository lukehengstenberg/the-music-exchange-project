using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMusicExchangeProject.Areas.Identity.Data;

namespace TheMusicExchangeProject.Models
{
    public class Block
    {
        public int ID { get; set; }
        public virtual TheMusicExchangeProjectUser BlockFrom { get; set; }
        public virtual TheMusicExchangeProjectUser BlockTo { get; set; }
    }
}
