﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheMusicExchangeProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace TheMusicExchangeProject.Controllers
{
    [Authorize]
    [RequireHttps]
    public class AccountController : Controller
    {
        private TheMusicExchangeProjectContext _context;
        private readonly UserManager<TheMusicExchangeProjectUser> _userManager;

        public AccountController(TheMusicExchangeProjectContext context, UserManager<TheMusicExchangeProjectUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string searchString, string selectedLevel)
        {
            ViewData["skillFilter"] = searchString;
            ViewData["levelFilter"] = selectedLevel;

            var skills = from s in _context.Skills
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                skills = skills.Where(s => s.SkillName.Contains(searchString.ToUpper()));
                if (!String.IsNullOrEmpty(selectedLevel))
                {
                    if (selectedLevel.Equals("1"))
                    {
                        skills = skills.Where(s => s.SkillName.Contains(searchString.ToUpper()) && s.Level.Name.Contains("Beginner"));
                    }
                    if (selectedLevel.Equals("2"))
                    {
                        skills = skills.Where(s => s.SkillName.Contains(searchString.ToUpper()) && s.Level.Name.Contains("Intermediate"));
                    }
                    if (selectedLevel.Equals("3"))
                    {
                        skills = skills.Where(s => s.SkillName.Contains(searchString.ToUpper()) && s.Level.Name.Contains("Advanced"));
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
                            where o.Id.Equals(o2.UserID) select new UserSkillsViewModel {
                                Users = o, Skills = o2, SkillLevel = o2.Level.Name, Age = CalculateAge(o.DOB),
                                Distance = CalculateDistance.BetweenTwoPostCodes(currentUser.Postcode, o.Postcode, CalculateDistance.Units.Miles)};

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

            return View(viewModel.Where(u => u.Users.Id != userId && 
            !connections.Any(c => (c.RequestFrom.Equals(currentUser) && c.RequestTo.Equals(u.Users))
            || (c.RequestTo.Equals(currentUserx) && c.RequestFrom.Equals(u.Users) && c.IsConfirmed.Equals(true)))
            && !blocks.Any(b => (b.BlockFrom.Equals(currentUser) && b.BlockTo.Equals(u.Users))
            || (b.BlockFrom.Equals(u.Users) && b.BlockTo.Equals(currentUserx)))));
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
            
            var conExist = await connections.Where(u => u.RequestFrom.Equals(targetUser) && u.RequestTo.Equals(currentUser)).FirstOrDefaultAsync();
            if(conExist != null)
            {
                conExist.IsConfirmed = true;
            }
            else
            {
                var c = await connections.Where(u => u.RequestFrom.Equals(currentUser) && u.RequestTo.Equals(targetUser)).FirstOrDefaultAsync();
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

        public async Task<IActionResult> Block(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var username = User.Identity.Name;
            TheMusicExchangeProjectUser currentUser = await _userManager.FindByNameAsync(username);
            TheMusicExchangeProjectUser targetUser = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);

            var blocks = _context.Blocks;
            Block newBlock = new Block
            {
                BlockFrom = currentUser,
                BlockTo = targetUser
            };
            blocks.Add(newBlock);

            var connections = _context.Connections;

            var conExistTo = await connections.Where(u => u.RequestFrom.Equals(currentUser) && u.RequestTo.Equals(targetUser)).FirstOrDefaultAsync();
            if(conExistTo != null)
            {
                var con = await _context.Connections.FindAsync(conExistTo.ID);
                _context.Connections.Remove(con);
                await _context.SaveChangesAsync();
            }
            var conExistFrom = await connections.Where(u => u.RequestFrom.Equals(targetUser) && u.RequestTo.Equals(currentUser)).FirstOrDefaultAsync();
            if (conExistFrom != null)
            {
                var con = await _context.Connections.FindAsync(conExistFrom.ID);
                _context.Connections.Remove(con);
                await _context.SaveChangesAsync();
            }

            _context.SaveChanges();
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

            var conExistTo = await connections.Where(u => u.RequestFrom.Id.Equals(currentUser.Id) && u.RequestTo.Id.Equals(id)).FirstOrDefaultAsync();
            if (conExistTo != null)
            {
                var con = await _context.Connections.FindAsync(conExistTo.ID);
                _context.Connections.Remove(con);
                await _context.SaveChangesAsync();
            }
            var conExistFrom = await connections.Where(u => u.RequestFrom.Id.Equals(id) && u.RequestTo.Id.Equals(currentUser.Id)).FirstOrDefaultAsync();
            if (conExistFrom != null)
            {
                var con = await _context.Connections.FindAsync(conExistFrom.ID);
                _context.Connections.Remove(con);
                await _context.SaveChangesAsync();
            }
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
    }
}