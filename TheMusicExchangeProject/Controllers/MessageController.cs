using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PusherServer;
using TheMusicExchangeProject.Models;

namespace TheMusicExchangeProject.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly TheMusicExchangeProjectContext _context;
        private readonly UserManager<TheMusicExchangeProjectUser> _userManager;
        public MessageController(TheMusicExchangeProjectContext context, UserManager<TheMusicExchangeProjectUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet("{group_id}")]
        public IEnumerable<Message> GetById(int group_id)
        {
            return _context.Messages.Where(gb => gb.GroupId == group_id);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MessageViewModel message)
        {
            Message new_message = new Message { AddedBy = _userManager.GetUserName(User), message = message.message, GroupId = message.GroupId };

            _context.Messages.Add(new_message);
            _context.SaveChanges();

            var options = new PusherOptions
            {
                Cluster = "eu",
                Encrypted = true
            };
            var pusher = new Pusher(
                "732466",
                "a334995a12f8ba424958",
                "c010b1f4ce0773c42e8f",
                options
            );
            var result = await pusher.TriggerAsync(
                "private-" + message.GroupId,
                "new_message",
            new { new_message },
            new TriggerOptions() { SocketId = message.SocketId });

            return new ObjectResult(new { status = "success", data = new_message });
        }
    }
}