using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheMusicExchangeProject.Models;

namespace TheMusicExchangeProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly TheMusicExchangeProjectContext _context;

        public AccountController(TheMusicExchangeProjectContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["skillFilter"] = searchString;

            var skills = from s in _context.Skills
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                skills = skills.Where(s => s.SkillName.Contains(searchString.ToUpper()));
            }
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
    }
}