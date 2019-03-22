using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheMusicExchangeProject.Models;
using Microsoft.AspNetCore.Identity;
using TheMusicExchangeProject.Areas.Identity.Data;
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

            var viewModel = from o in _context.Users join o2 in skills on o.Id equals o2.UserID where o.Id.Equals(o2.UserID) select new UserSkillsViewModel { Users = o, Skills = o2, SkillLevel = o2.Level.Name, Age = CalculateAge(o.DOB)};
            return View(viewModel);
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
    }
}