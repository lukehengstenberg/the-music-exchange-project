using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMusicExchangeProject.Areas.Identity.Data;

namespace TheMusicExchangeProject.Models
{
    public class UserBlocksViewModel
    {
        public TheMusicExchangeProjectUser Users { get; set; }
        public Block Block { get; set; }
    }
}
