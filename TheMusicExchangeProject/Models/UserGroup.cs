using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMusicExchangeProject.Models
{
    public class UserGroup
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public int GroupId { get; set; }

        public TheMusicExchangeProjectUser User { get; set; }
        public MessageGroup MessageGroup { get; set; }
    }
}
