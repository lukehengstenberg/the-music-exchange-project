using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMusicExchangeProject.Areas.Identity.Data;

namespace TheMusicExchangeProject.Models
{
    public class UserConnectionsViewModel
    {
        public TheMusicExchangeProjectUser Users { get; set; }
        public Connection Connection { get; set; }
    }
}
