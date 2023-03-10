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
    [Route("api/[controller]")]
    [Authorize]
    public class GroupController : Controller
    {
        private readonly TheMusicExchangeProjectContext _context;
        private readonly UserManager<TheMusicExchangeProjectUser> _userManager;

        public GroupController(TheMusicExchangeProjectContext context, UserManager<TheMusicExchangeProjectUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<UserGroupViewModel> GetAll()
        {

            var groups = _context.UserGroups
                          .Where(gp => gp.UserName == _userManager.GetUserName(User))
                          .Join(_context.MessageGroups, ug => ug.GroupId, g => g.ID, (ug, g) =>
                                         new UserGroupViewModel()
                                        {
                                            UserName = ug.UserName,
                                            GroupId = g.ID,
                                            GroupName = g.GroupName
                                        })
                           .ToList();

            return groups;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewGroupViewModel group)
        {
            if (group == null || group.GroupName == "")
            {
                return new ObjectResult(
                    new { status = "error", message = "incomplete request" }
                );
            }
            if ((_context.MessageGroups.Any(gp => gp.GroupName == group.GroupName)) == true)
            {
                return new ObjectResult(
                    new { status = "error", message = "group name already exist" }
                );
            }

            MessageGroup newGroup = new MessageGroup { GroupName = group.GroupName };
            
            _context.MessageGroups.Add(newGroup);
            _context.SaveChanges();
            
            _context.UserGroups.Add(
                new UserGroup { UserName = _userManager.GetUserName(User), GroupId = newGroup.ID }
                );
            foreach (string UserName in group.UserNames)
            {
                _context.UserGroups.Add(
                    new UserGroup { UserName = UserName, GroupId = newGroup.ID }
                );
                _context.SaveChanges();
            }
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
                "new_group", // event name
            new { newGroup });

            return new ObjectResult(new { status = "success", data = newGroup });
        }
    }
}