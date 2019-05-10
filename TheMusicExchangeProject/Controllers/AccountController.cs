using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheMusicExchangeProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting;
using PusherServer;

namespace TheMusicExchangeProject.Controllers
{
    [Authorize]
    [RequireHttps]
    public class AccountController : Controller
    {
        private TheMusicExchangeProjectContext _context;
        private readonly UserManager<TheMusicExchangeProjectUser> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AccountController(TheMusicExchangeProjectContext context, UserManager<TheMusicExchangeProjectUser> userManager, 
            IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Index(string searchString, string selectedLevel)
        {
            ViewData["skillFilter"] = searchString;
            ViewData["levelFilter"] = selectedLevel;

            var skills = from s in _context.Skills
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                skills = skills
                    .Where(s => s.SkillName
                        .Contains(searchString.ToUpper()));
                if (!String.IsNullOrEmpty(selectedLevel))
                {
                    if (selectedLevel.Equals("1"))
                    {
                        skills = skills
                            .Where(s => s.SkillName
                                .Contains(searchString.ToUpper()) && 
                                    s.Level.Name.Contains("Beginner"));
                    }
                    if (selectedLevel.Equals("2"))
                    {
                        skills = skills
                            .Where(s => s.SkillName
                                .Contains(searchString.ToUpper()) && 
                                    s.Level.Name.Contains("Intermediate"));
                    }
                    if (selectedLevel.Equals("3"))
                    {
                        skills = skills
                            .Where(s => s.SkillName
                                .Contains(searchString.ToUpper()) && 
                                    s.Level.Name.Contains("Advanced"));
                    }
                }
            }
            if(String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(selectedLevel))
            {
                if (selectedLevel.Equals("1"))
                {
                    skills = skills.Where(s => s.Level.Name.Contains("Beginner"));
                }
                if (selectedLevel.Equals("2"))
                {
                    skills = skills.Where(s => s.Level.Name.Contains("Intermediate"));
                }
                if (selectedLevel.Equals("3"))
                {
                    skills = skills.Where(s => s.Level.Name.Contains("Advanced"));
                }
            }

            ViewBag.levels = new SelectList(new[]
            {
                new{ id = "", level = "All"},
                new{ id = "1", level = "Beginner"},
                new{ id = "2", level = "Intermediate"},
                new{ id = "3", level = "Advanced"},
            },
            "id", "level", selectedLevel);

            var username = User.Identity.Name;
            TheMusicExchangeProjectUser currentUser = await _userManager.FindByNameAsync(username);
            TheMusicExchangeProjectUser currentUserx = await _userManager.FindByNameAsync(username);
            var userId = currentUser.Id;

            var connections = _context.Connections;
           
            var blocks = _context.Blocks;

            var viewModel = from o in _context.Users join o2 in skills on o.Id equals o2.UserID
                            where o.Id.Equals(o2.UserID)
                            select new UserSkillsViewModel {
                                Users = o, Skills = o2, SkillLevel = o2.Level.Name, Age = CalculateAge(o.DOB),
                                Distance = CalculateDistance.BetweenTwoPostCodes(
                                    currentUser.Latitude, currentUser.Longitude, 
                                    o.Latitude, o.Longitude, CalculateDistance.Units.Miles)};

            List<UserConnectionsViewModel> userConnectionsTo = await (from o  in _context.Users
                                  join o2 in connections on o.Id 
                                  equals o2.RequestTo.Id
                                  where o.Id.Equals(o2.RequestTo.Id) && o2.RequestFrom.Id.Equals(userId) && o2.IsConfirmed.Equals(true)
                                  select new UserConnectionsViewModel { Users = o, Connection = o2}).ToListAsync();

            List <UserConnectionsViewModel> userConnectionsFrom = await (from o in _context.Users
                                      join o2 in connections on o.Id
                                      equals o2.RequestFrom.Id
                                      where o.Id.Equals(o2.RequestFrom.Id) && o2.RequestTo.Id.Equals(userId) && o2.IsConfirmed.Equals(true)
                                      select new UserConnectionsViewModel { Users = o, Connection = o2 }).ToListAsync();
            var userConnections = userConnectionsTo.Concat(userConnectionsFrom);
            ViewBag.conData = userConnections;

            var userBlocks = await (from o in _context.Users
                             join o2 in blocks on o.Id
                             equals o2.BlockTo.Id
                             where o2.BlockFrom.Id.Equals(userId) && o.Id.Equals(o2.BlockTo.Id)
                             select new UserBlocksViewModel { Users = o, Block = o2 }).ToListAsync();
            ViewBag.blkData = userBlocks;

            IEnumerable<UserConnectionsViewModel> userRequests = await (from o in _context.Users
                                                                 join o2 in connections on o.Id
                                                                 equals o2.RequestFrom.Id
                                                                 where o.Id.Equals(o2.RequestFrom.Id) && o2.RequestTo.Id.Equals(userId) && o2.IsConfirmed.Equals(false)
                                                                 && !blocks.Any(b => (b.BlockFrom.Equals(currentUser) && b.BlockTo.Equals(o2.RequestFrom)))
                                                                 select new UserConnectionsViewModel { Users = o, Connection = o2 }).ToListAsync();
            ViewBag.reqData = userRequests;

            return View(viewModel.Where(u => u.Users.Id != userId && 
            !connections.Any(c => (c.RequestFrom.Equals(currentUser) && c.RequestTo.Equals(u.Users))
            || (c.RequestTo.Equals(currentUserx) && c.RequestFrom.Equals(u.Users) && c.IsConfirmed.Equals(true)))
            && !blocks.Any(b => (b.BlockFrom.Equals(currentUser) && b.BlockTo.Equals(u.Users))
            || (b.BlockFrom.Equals(u.Users) && b.BlockTo.Equals(currentUserx)))).OrderBy(u => Guid.NewGuid()));
        }
        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if(DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            {
                age = age - 1;
            }
            return age;
        }
        /**
         * 
         * Method creating a connection between two users.
         * Either creates a new connection entity or confirms an existing one.
         *
         */
        public async Task<IActionResult> Connect(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var username = User.Identity.Name;
            TheMusicExchangeProjectUser currentUser = await _userManager.FindByNameAsync(username);
            TheMusicExchangeProjectUser targetUser = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);

            var connections = _context.Connections;
            
            var conExist = await connections
                .Where(u => u.RequestFrom.Equals(targetUser) && u.RequestTo.Equals(currentUser))
                .FirstOrDefaultAsync();

            if (conExist != null)
            {
                conExist.IsConfirmed = true;
            }
            else
            {
                var c = await connections
                    .Where(u => u.RequestFrom.Equals(currentUser) && u.RequestTo.Equals(targetUser))
                    .FirstOrDefaultAsync();
                if (c == null)
                {
                    Connection newConnection = new Connection
                    {
                        RequestFrom = currentUser,
                        RequestTo = targetUser,
                        IsConfirmed = false
                    };
                    connections.Add(newConnection);
                }
            }
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        /**
         * 
         * Method creating a block between two users.
         * Creates a new block entity and removes any existing connection. 
         *
         */
        public async Task<IActionResult> Block(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Gets the current and target user from the database.
            var username = User.Identity.Name;
            TheMusicExchangeProjectUser currentUser = await _userManager.FindByNameAsync(username);
            TheMusicExchangeProjectUser targetUser = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            
            // Creates a new Block entity and adds to Blocks table.
            var blocks = _context.Blocks;
            Block newBlock = new Block
            {
                BlockFrom = currentUser,
                BlockTo = targetUser
            };
            blocks.Add(newBlock);

            var connections = _context.Connections;
            // Checks for any existing connections to the user.
            var conExistTo = await connections
                .Where(u => u.RequestFrom.Equals(currentUser) && u.RequestTo.Equals(targetUser))
                .FirstOrDefaultAsync();
            // Removes connection from the database.
            if (conExistTo != null)
            {
                var con = await _context.Connections.FindAsync(conExistTo.ID);
                _context.Connections.Remove(con);
            }
            // Checks for any existing connections from the user.
            var conExistFrom = await connections
                .Where(u => u.RequestFrom.Equals(targetUser) && u.RequestTo.Equals(currentUser))
                .FirstOrDefaultAsync();
            // Removes connection from the database.
            if (conExistFrom != null)
            {
                var con = await _context.Connections.FindAsync(conExistFrom.ID);
                _context.Connections.Remove(con);
            }
            

            // Saves all changes to the database and updates page.
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Disconnect(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var connections = _context.Connections;
            var username = User.Identity.Name;
            TheMusicExchangeProjectUser currentUser = await _userManager.FindByNameAsync(username);
            TheMusicExchangeProjectUser targetUser = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);

            var conExistTo = await connections
                .Where(u => u.RequestFrom.Id.Equals(currentUser.Id) && u.RequestTo.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (conExistTo != null)
            {
                var con = await _context.Connections.FindAsync(conExistTo.ID);
                _context.Connections.Remove(con);
            }
            var conExistFrom = await connections
                .Where(u => u.RequestFrom.Id.Equals(id) && u.RequestTo.Id.Equals(currentUser.Id))
                .FirstOrDefaultAsync();
            if (conExistFrom != null)
            {
                var con = await _context.Connections.FindAsync(conExistFrom.ID);
                _context.Connections.Remove(con);
            }
            //var currentUserGroups = _context.UserGroups
            //    .Where(u => u.UserName == currentUser.UserName);
            //var targetUserGroups = _context.UserGroups
            //    .Where(u => u.UserName == targetUser.UserName);
            //var messageGroups = from o in currentUserGroups
            //                 join o2 in targetUserGroups on o.GroupId equals o2.GroupId
            //                 select new MessageGroup {ID = o.GroupId};
            
            //if (messageGroups != null)
            //{
            //    foreach (MessageGroup messageGroup in messageGroups)
            //    {
            //        var userGroups = _context.UserGroups.Where(u => u.GroupId == messageGroup.ID);
            //        var messages = _context.Messages.Where(m => m.GroupId == messageGroup.ID);

            //        _context.MessageGroups.Remove(messageGroup);
            //        foreach (UserGroup userGroup in userGroups)
            //        {
            //            _context.UserGroups.Remove(userGroup);
            //        }
            //        foreach (Message message in messages)
            //        {
            //            _context.Messages.Remove(message);
            //        }
            //        var options = new PusherOptions
            //        {
            //            Cluster = "eu",
            //            Encrypted = true
            //        };
            //        var pusher = new Pusher(
            //            "732466",
            //            "a334995a12f8ba424958",
            //            "c010b1f4ce0773c42e8f",
            //            options
            //        );
            //        var result = await pusher.TriggerAsync(
            //            "group_chat", //channel name
            //            "delete_group", // event name
            //            new { id });
            //    }
            //}
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Unblock(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var blocks = _context.Blocks;
            var username = User.Identity.Name;
            TheMusicExchangeProjectUser currentUser = await _userManager.FindByNameAsync(username);

            var block = await blocks.Where(u => u.BlockFrom.Id.Equals(currentUser.Id) && u.BlockTo.Id.Equals(id)).FirstOrDefaultAsync();
            _context.Blocks.Remove(block);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public FileContentResult ProfilePictures()
        {
            var username = User.Identity.Name;
            var currentUser = _context.Users.Where(u => u.UserName == username).FirstOrDefault();
            if (currentUser.ProfilePicture == null)
            {
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "images", $"blank_profile.png");
                byte[] imageData = System.IO.File.ReadAllBytes(path);
                return new FileContentResult(imageData, "image/jpeg");
            }
            return new FileContentResult(currentUser.ProfilePicture, "image/jpeg");
        }
        /**
         * 
         * Method for converting the ProfilePicture byte array to an image.
         * Takes the UserID as a parameter and returns an image file.
         *
         */
        public FileContentResult GenerateProfilePictures(string id)
        {
            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            if (user.ProfilePicture == null)
            {
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "images", $"blank_profile.png");
                byte[] imageData = System.IO.File.ReadAllBytes(path);
                return new FileContentResult(imageData, "image/jpeg");
            }
            return new FileContentResult(user.ProfilePicture, "image/jpeg");
        }
    }
}