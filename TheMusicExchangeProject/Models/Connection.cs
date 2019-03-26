using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMusicExchangeProject.Models;

namespace TheMusicExchangeProject.Models
{
    public class Connection
    {
        public int ID { get; set; }
        public virtual TheMusicExchangeProjectUser RequestFrom { get; set; }
        public virtual TheMusicExchangeProjectUser RequestTo { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
