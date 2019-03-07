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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

    }
}