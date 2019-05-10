using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PusherServer;
using TheMusicExchangeProject.Models;

namespace TheMusicExchangeProject.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly UserManager<TheMusicExchangeProjectUser> _userManager;
        private readonly TheMusicExchangeProjectContext _context;
        public ChatController(
            UserManager<TheMusicExchangeProjectUser> userManager,
            TheMusicExchangeProjectContext context
            )
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var groups = _context.UserGroups
                            .Where(gp => gp.UserName == _userManager.GetUserName(User))
                            .Join(_context.MessageGroups, ug => ug.GroupId, g => g.ID, (ug, g) =>
                                new UserGroupViewModel
                                {
                                    UserName = ug.UserName,
                                    GroupId = g.ID,
                                    GroupName = g.GroupName
                                }).ToList();

            ViewData["UserGroups"] = groups;

            var username = User.Identity.Name;
            TheMusicExchangeProjectUser currentUser = await _userManager.FindByNameAsync(username);
            var userId = currentUser.Id;
            var connections = _context.Connections;
            
            ViewData["Users"] = _userManager.Users.Where(u => u.Id != userId &&
            connections.Any(c => (c.RequestFrom.Id.Equals(currentUser.Id) && c.RequestTo.Id.Equals(u.Id) && c.IsConfirmed.Equals(true))
            || (c.RequestTo.Id.Equals(currentUser.Id) && c.RequestFrom.Id.Equals(u.Id) && c.IsConfirmed.Equals(true))));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var messageGroup = await _context.MessageGroups.FindAsync(id);
            var userGroups = _context.UserGroups.Where(u => u.GroupId == id);
            var messages = _context.Messages.Where(m => m.GroupId == id);

            _context.MessageGroups.Remove(messageGroup);
            foreach (UserGroup userGroup in userGroups)
            {
                _context.UserGroups.Remove(userGroup);
            }
            foreach (Message message in messages)
            {
                _context.Messages.Remove(message);
            }
            await _context.SaveChangesAsync();
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
                "group_chat", //channel name
                "delete_group", // event name
                new { id });

            //return new ObjectResult(new { status = "success", data = id });
            return NoContent();
        }
    }
}