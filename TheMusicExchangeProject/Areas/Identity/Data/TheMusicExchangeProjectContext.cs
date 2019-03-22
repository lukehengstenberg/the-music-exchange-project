using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheMusicExchangeProject.Areas.Identity.Data;

namespace TheMusicExchangeProject.Models
{
    public class TheMusicExchangeProjectContext : IdentityDbContext<TheMusicExchangeProjectUser>
    {
        public TheMusicExchangeProjectContext(DbContextOptions<TheMusicExchangeProjectContext> options)
            : base(options)
        {
        }

        public DbSet<SkillLevel> SkillLevels { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Block> Blocks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<SkillLevel>().ToTable("SkillLevel");
            builder.Entity<Skill>().ToTable("Skill");
        }
    }
}
