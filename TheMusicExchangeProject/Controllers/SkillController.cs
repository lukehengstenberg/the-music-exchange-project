using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheMusicExchangeProject.Areas.Identity.Data;
using TheMusicExchangeProject.Models;

namespace TheMusicExchangeProject.Controllers
{
    public class SkillController : Controller
    {
        private readonly TheMusicExchangeProjectContext _context;
        private readonly UserManager<TheMusicExchangeProjectUser> _userManager;

        public SkillController(TheMusicExchangeProjectContext context, UserManager<TheMusicExchangeProjectUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var skills = from s in _context.Skills
                         select s;
            var username = User.Identity.Name;
            if (!String.IsNullOrEmpty(username))
            {
                TheMusicExchangeProjectUser currentUser = await _userManager.FindByNameAsync(username);
                skills = skills.Where(s => s.UserID.Contains(currentUser.Id));
            }
            var viewModel = from o in _context.Users join o2 in skills on o.Id equals o2.UserID where o.Id.Equals(o2.UserID) select new UserSkillsViewModel { Users = o, Skills = o2, SkillLevel = o2.Level.Name};
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.levels = new SelectList(new[]
            {
                new{ id = "1", level = "Beginner"},
                new{ id = "2", level = "Intermediate"},
                new{ id = "3", level = "Advanced"},
            },
            "id", "level");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SkillName,UserID,Description")] Skill skill, string selectedLevel)
        {
            ViewData["skillLevel"] = selectedLevel;
            
            if (ModelState.IsValid)
            {
                skill.SkillName = skill.SkillName;
                var username = User.Identity.Name;
                TheMusicExchangeProjectUser currentUser = await _userManager.FindByNameAsync(username);
                skill.UserID = currentUser.Id;
                skill.LevelID = int.Parse(selectedLevel);
                skill.Description = skill.Description;

                skill.User = currentUser;
                var levels = from l in _context.SkillLevels
                             select l;

                SkillLevel currentLevel = await levels.Where(l => l.LevelID.Equals(int.Parse(selectedLevel))).SingleOrDefaultAsync();
                skill.Level = currentLevel;
                _context.Add(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skill);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills
                .FirstOrDefaultAsync(m => m.SkillID == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}