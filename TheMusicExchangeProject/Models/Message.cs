using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMusicExchangeProject.Models
{
    public class Message
    {
        public int ID { get; set; }
        public string AddedBy { get; set; }
        public string message { get; set; }
        public int GroupId { get; set; }
        public DateTime TimeSent { get; set; }

        public TheMusicExchangeProjectUser User { get; set; }
        public MessageGroup MessageGroup { get; set; }
    }
}
