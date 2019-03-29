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
    public class AuthController : Controller
    {
        private readonly TheMusicExchangeProjectContext _context;
        private readonly UserManager<TheMusicExchangeProjectUser> _userManager;

        public AuthController(TheMusicExchangeProjectContext context, UserManager<TheMusicExchangeProjectUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult ChannelAuth(string channel_name, string socket_id)
        {
            int group_id;
            if (!User.Identity.IsAuthenticated)
            {
                return new ContentResult { Content = "Access forbidden", ContentType = "application/json" };
            }

            try
            {
                group_id = Int32.Parse(channel_name.Replace("private-", ""));
            }
            catch (FormatException e)
            {
                return Json(new { Content = e.Message });
            }

            var IsInChannel = _context.UserGroups
                                      .Where(gb => gb.GroupId == group_id
                                            && gb.UserName == _userManager.GetUserName(User))
                                      .Count();

            if (IsInChannel > 0)
            {
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

                var auth = pusher.Authenticate(channel_name, socket_id).ToJson();
                return new ContentResult { Content = auth, ContentType = "application/json" };
            }
            return new ContentResult { Content = "Access forbidden", ContentType = "application/json" };
        }
    }
}