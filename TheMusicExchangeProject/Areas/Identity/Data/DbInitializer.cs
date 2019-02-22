using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMusicExchangeProject.Models;

namespace TheMusicExchangeProject.Areas.Identity.Data
{
    public static class DbInitializer
    {
        public static void SeedData
            (UserManager<TheMusicExchangeProjectUser> userManager,
            TheMusicExchangeProjectContext context)
        {
            SeedUsers(userManager);
            SeedSkills(userManager, context);
        }

        public static void SeedUsers
            (UserManager<TheMusicExchangeProjectUser> userManager)
        {
            if(userManager.FindByEmailAsync("bob@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "bob@email.com";
                user.Email = "bob@email.com";
                user.Name = "Bob";
                user.Bio = "Hi I'm Bob, I love to play Guitar and the Flute!";
                user.DOB = new DateTime(1996, 1, 1);
                user.Postcode = "SA11DP";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("jim@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "jim@email.com";
                user.Email = "jim@email.com";
                user.Name = "Jim";
                user.Bio = "Hi I'm Jim, I love to play the Flute and am learning Trombone!";
                user.DOB = new DateTime(1996, 10, 14);
                user.Postcode = "SA20AA";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("steve@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "steve@email.com";
                user.Email = "steve@email.com";
                user.Name = "Steve";
                user.Bio = "Hi I'm Steve, I'm learning Piano.";
                user.DOB = new DateTime(1989, 4, 10);
                user.Postcode = "SA31AA";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("thom@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "thom@email.com";
                user.Email = "thom@email.com";
                user.Name = "Thom";
                user.Bio = "Hi I'm Thom, I've been playing Guitar, Piano and Drums for 10 years.";
                user.DOB = new DateTime(1997, 4, 20);
                user.Postcode = "SA20QX";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("luke@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "luke@email.com";
                user.Email = "luke@email.com";
                user.Name = "Luke";
                user.Bio = "Hi I'm Luke, I can play Piano and Violin.";
                user.DOB = new DateTime(1996, 8, 16);
                user.Postcode = "SA41GE";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("freddie@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "freddie@email.com";
                user.Email = "freddie@email.com";
                user.Name = "Freddie";
                user.Bio = "Hi I'm Freddie, I enjoy playing the Trombone!";
                user.DOB = new DateTime(1994, 10, 10);
                user.Postcode = "SA31AA";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("georgia@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "georgia@email.com";
                user.Email = "georgia@email.com";
                user.Name = "Georgia";
                user.Bio = "Hi I'm Georgia, I'm learning to play the Ukulele.";
                user.DOB = new DateTime(1996, 8, 12);
                user.Postcode = "SA54AA";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("rhodri@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "rhodri@email.com";
                user.Email = "rhodri@email.com";
                user.Name = "Rhodri";
                user.Bio = "Hi I'm Rhodri, I'm an expert at playing piano, guitar and saxophone.";
                user.DOB = new DateTime(1993, 5, 12);
                user.Postcode = "SA65RH";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("ellie@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "ellie@email.com";
                user.Email = "ellie@email.com";
                user.Name = "Ellie";
                user.Bio = "Hi I'm Ellie, I enjoy playing Violin and Ukulele";
                user.DOB = new DateTime(1992, 8, 4);
                user.Postcode = "SA70AA";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("jessica@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "jessica@email.com";
                user.Email = "jessica@email.com";
                user.Name = "Jessica";
                user.Bio = "Hi I'm Jess, I love to play Guitar!";
                user.DOB = new DateTime(1998, 12, 1);
                user.Postcode = "SA84DA";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("liam@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "liam@email.com";
                user.Email = "liam@email.com";
                user.Name = "Liam";
                user.Bio = "Hi I'm Liam, I'm learning how to play the drums.";
                user.DOB = new DateTime(1988, 6, 10);
                user.Postcode = "SA91BU";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("greg@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "greg@email.com";
                user.Email = "greg@email.com";
                user.Name = "Greg";
                user.Bio = "Hi I'm Greg, I've been playing Guitar for 4 years, Piano for 7 years and Violin for 5 years";
                user.DOB = new DateTime(1996, 9, 23);
                user.Postcode = "SA89AY";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("francis@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "francis@email.com";
                user.Email = "francis@email.com";
                user.Name = "Francis";
                user.Bio = "Hi I'm Francis, I play Trombone and Saxophone.";
                user.DOB = new DateTime(1992, 7, 7);
                user.Postcode = "SA79YU";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("jordan@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "jordan@email.com";
                user.Email = "jordan@email.com";
                user.Name = "Jordan";
                user.Bio = "Hi I'm Jordan, I've been playing Flute and Ukulele for 2 years.";
                user.DOB = new DateTime(1998, 10, 10);
                user.Postcode = "SA65LT";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("ben@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "ben@email.com";
                user.Email = "ben@email.com";
                user.Name = "Ben";
                user.Bio = "Hi I'm Ben, I'm learning to play the Saxophone.";
                user.DOB = new DateTime(1991, 7, 12);
                user.Postcode = "SA54TW";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("dave@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "dave@email.com";
                user.Email = "dave@email.com";
                user.Name = "Dave";
                user.Bio = "Hi I'm Dave, I rock out on Flute, Drums and Trombone";
                user.DOB = new DateTime(1993, 11, 11);
                user.Postcode = "SA40ZL";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("matt@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "matt@email.com";
                user.Email = "matt@email.com";
                user.Name = "Matt";
                user.Bio = "Hi I'm Matt, hit me up to jam, I play Guitar and Ukulele.";
                user.DOB = new DateTime(1995, 7, 13);
                user.Postcode = "SA32HB";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("ross@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "ross@email.com";
                user.Email = "ross@email.com";
                user.Name = "Ross";
                user.Bio = "Hi I'm Ross, you'll catch me playing Flute, Drums or Violin.";
                user.DOB = new DateTime(1995, 11, 30);
                user.Postcode = "SA20RE";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("toby@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "toby@email.com";
                user.Email = "toby@email.com";
                user.Name = "Toby";
                user.Bio = "Hi I'm Toby, I play Saxophone and I'm looking for someone to learn Trombone with me!";
                user.DOB = new DateTime(1997, 3, 13);
                user.Postcode = "SA11NH";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
            if (userManager.FindByEmailAsync("sarah@email.com").Result == null)
            {
                TheMusicExchangeProjectUser user = new TheMusicExchangeProjectUser();
                user.UserName = "sarah@email.com";
                user.Email = "sarah@email.com";
                user.Name = "Sarah";
                user.Bio = "Hi I'm Sarah, I play Drums, Saxophone, Violin and Ukulele.";
                user.DOB = new DateTime(1997, 5, 15);
                user.Postcode = "SA29BS";

                IdentityResult result = userManager.CreateAsync
                    (user, "Password123!").Result;
            }
        }

        public static void SeedSkills
            (UserManager<TheMusicExchangeProjectUser> userManager,
            TheMusicExchangeProjectContext context)
        {
            context.Database.EnsureCreated();
            if (context.SkillLevels.Any())
            {
                return; 
            }
            var skillLevels = new SkillLevel[]
            {
                new SkillLevel{Name="Beginner"},
                new SkillLevel{Name="Intermediate"},
                new SkillLevel{Name="Advanced"}
            };
            foreach (SkillLevel l in skillLevels)
            {
                context.SkillLevels.Add(l);
            }
            context.SaveChanges();

            var skills = new Skill[]
            {
                new Skill{UserID=userManager.FindByEmailAsync("bob@email.com").Result.Id,
                    SkillName="GUITAR", LevelID=2, Description="Instrument: Guitar"},
                new Skill{UserID=userManager.FindByEmailAsync("bob@email.com").Result.Id,
                    SkillName="FLUTE", LevelID=2, Description="Instrument: Flute"},

                new Skill{UserID=userManager.FindByEmailAsync("jim@email.com").Result.Id,
                    SkillName="FLUTE", LevelID=2, Description="Instrument: Flute"},
                new Skill{UserID=userManager.FindByEmailAsync("jim@email.com").Result.Id,
                    SkillName="TROMBONE", LevelID=1, Description="Instrument: Trombone"},

                new Skill{UserID=userManager.FindByEmailAsync("steve@email.com").Result.Id,
                    SkillName="PIANO", LevelID=1, Description="Instrument: Piano"},

                new Skill{UserID=userManager.FindByEmailAsync("thom@email.com").Result.Id,
                    SkillName="GUITAR", LevelID=3, Description="Instrument: Guitar"},
                new Skill{UserID=userManager.FindByEmailAsync("thom@email.com").Result.Id,
                    SkillName="PIANO", LevelID=3, Description="Instrument: Piano"},
                new Skill{UserID=userManager.FindByEmailAsync("thom@email.com").Result.Id,
                    SkillName="DRUMS", LevelID=2, Description="Instrument: Drums"},

                new Skill{UserID=userManager.FindByEmailAsync("luke@email.com").Result.Id,
                    SkillName="PIANO", LevelID=2, Description="Instrument: Piano"},
                new Skill{UserID=userManager.FindByEmailAsync("luke@email.com").Result.Id,
                    SkillName="VIOLIN", LevelID=2, Description="Instrument: Violin"},

                new Skill{UserID=userManager.FindByEmailAsync("freddie@email.com").Result.Id,
                    SkillName="TROMBONE", LevelID=2, Description="Instrument: Trombone"},

                new Skill{UserID=userManager.FindByEmailAsync("georgia@email.com").Result.Id,
                    SkillName="UKULELE", LevelID=1, Description="Instrument: Ukulele"},

                new Skill{UserID=userManager.FindByEmailAsync("rhodri@email.com").Result.Id,
                    SkillName="PIANO", LevelID=3, Description="Instrument: Piano"},
                new Skill{UserID=userManager.FindByEmailAsync("rhodri@email.com").Result.Id,
                    SkillName="GUITAR", LevelID=2, Description="Instrument: Guitar"},
                new Skill{UserID=userManager.FindByEmailAsync("rhodri@email.com").Result.Id,
                    SkillName="SAXOPHONE", LevelID=3, Description="Instrument: Saxophone"},

                new Skill{UserID=userManager.FindByEmailAsync("ellie@email.com").Result.Id,
                    SkillName="VIOLIN", LevelID=2, Description="Instrument: Violin"},
                new Skill{UserID=userManager.FindByEmailAsync("ellie@email.com").Result.Id,
                    SkillName="UKULELE", LevelID=2, Description="Instrument: Ukulele"},

                new Skill{UserID=userManager.FindByEmailAsync("jessica@email.com").Result.Id,
                    SkillName="GUITAR", LevelID=2, Description="Instrument: Guitar"},

                new Skill{UserID=userManager.FindByEmailAsync("liam@email.com").Result.Id,
                    SkillName="DRUMS", LevelID=2, Description="Instrument: Drums"},

                new Skill{UserID=userManager.FindByEmailAsync("greg@email.com").Result.Id,
                    SkillName="GUITAR", LevelID=3, Description="Instrument: Guitar"},
                new Skill{UserID=userManager.FindByEmailAsync("greg@email.com").Result.Id,
                    SkillName="PIANO", LevelID=3, Description="Instrument: Piano"},
                new Skill{UserID=userManager.FindByEmailAsync("greg@email.com").Result.Id,
                    SkillName="VIOLIN", LevelID=2, Description="Instrument: Violin"},

                new Skill{UserID=userManager.FindByEmailAsync("francis@email.com").Result.Id,
                    SkillName="TROMBONE", LevelID=2, Description="Instrument: Trombone"},
                new Skill{UserID=userManager.FindByEmailAsync("francis@email.com").Result.Id,
                    SkillName="SAXOPHONE", LevelID=2, Description="Instrument: Saxophone"},

                new Skill{UserID=userManager.FindByEmailAsync("jordan@email.com").Result.Id,
                    SkillName="FLUTE", LevelID=2, Description="Instrument: Flute"},
                new Skill{UserID=userManager.FindByEmailAsync("jordan@email.com").Result.Id,
                    SkillName="UKULELE", LevelID=1, Description="Instrument: Ukulele"},

                new Skill{UserID=userManager.FindByEmailAsync("ben@email.com").Result.Id,
                    SkillName="SAXOPHONE", LevelID=1, Description="Instrument: Saxophone"},

                new Skill{UserID=userManager.FindByEmailAsync("dave@email.com").Result.Id,
                    SkillName="FLUTE", LevelID=3, Description="Instrument: Flute"},
                new Skill{UserID=userManager.FindByEmailAsync("dave@email.com").Result.Id,
                    SkillName="DRUMS", LevelID=3, Description="Instrument: Drums"},
                new Skill{UserID=userManager.FindByEmailAsync("dave@email.com").Result.Id,
                    SkillName="TROMBONE", LevelID=3, Description="Instrument: Trombone"},

                new Skill{UserID=userManager.FindByEmailAsync("matt@email.com").Result.Id,
                    SkillName="GUITAR", LevelID=2, Description="Instrument: Guitar"},
                new Skill{UserID=userManager.FindByEmailAsync("matt@email.com").Result.Id,
                    SkillName="UKULELE", LevelID=2, Description="Instrument: Ukulele"},

                new Skill{UserID=userManager.FindByEmailAsync("ross@email.com").Result.Id,
                    SkillName="FLUTE", LevelID=2, Description="Instrument: Flute"},
                new Skill{UserID=userManager.FindByEmailAsync("ross@email.com").Result.Id,
                    SkillName="DRUMS", LevelID=2, Description="Instrument: Drums"},
                new Skill{UserID=userManager.FindByEmailAsync("ross@email.com").Result.Id,
                    SkillName="VIOLIN", LevelID=1, Description="Instrument: Violin"},

                new Skill{UserID=userManager.FindByEmailAsync("toby@email.com").Result.Id,
                    SkillName="TROMBONE", LevelID=1, Description="Instrument: Trombone"},
                new Skill{UserID=userManager.FindByEmailAsync("toby@email.com").Result.Id,
                    SkillName="SAXOPHONE", LevelID=2, Description="Instrument: Saxophone"},

                new Skill{UserID=userManager.FindByEmailAsync("sarah@email.com").Result.Id,
                    SkillName="DRUMS", LevelID=3, Description="Instrument: Drums"},
                new Skill{UserID=userManager.FindByEmailAsync("sarah@email.com").Result.Id,
                    SkillName="SAXOPHONE", LevelID=3, Description="Instrument: Saxophone"},
                new Skill{UserID=userManager.FindByEmailAsync("sarah@email.com").Result.Id,
                    SkillName="VIOLIN", LevelID=2, Description="Instrument: Violin"},
                new Skill{UserID=userManager.FindByEmailAsync("sarah@email.com").Result.Id,
                    SkillName="UKULELE", LevelID=2, Description="Instrument: Ukulele"},
            };
            foreach (Skill s in skills)
            {
                context.Skills.Add(s);
            }
            context.SaveChanges();
        }
    }
}
