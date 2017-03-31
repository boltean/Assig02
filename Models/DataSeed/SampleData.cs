using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenithCore.Data;
using ZenithCore.Models.ZenithModels;

namespace ZenithCore.Models.DataSeed
{
    public class YourDbContextSeedData
    {
        private readonly ApplicationDbContext _context;

        public YourDbContextSeedData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async void SeedAdminUser()
        {


            var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "Admin"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "Admin" });
            }

            var ExistingUser = _context.Users.FirstOrDefault(p => p.Email == "a@a.a");



            if (!_context.Users.Any(u => u.UserName == "a@a.a"))
            {

                var userStore = new UserStore<ApplicationUser>(_context);
                //ApplicationUser user = new ApplicationUser();


                await userStore.AddToRoleAsync(_context.Users.FirstOrDefault(p => p.Email == "a@a.a"), "Admin");
            }

            await _context.SaveChangesAsync();

        }

        public static List<Activity> getActivities()
        {
            List<Activity> activities = new List<Activity>()
            {
                new Activity() {

                    ActivityDescription ="Seniors Golf Tournament",
                    CreationDate =System.DateTime.Now
                },
                new Activity() {

                    ActivityDescription = "Leadership General Assembly Meeting",
                    CreationDate = System.DateTime.Now
                },
                new Activity() {

                    ActivityDescription = "Youth Bowling Tournament",
                    CreationDate = System.DateTime.Now
                }
        };

            return activities;
        }

        public static List<Event> getEvents(ApplicationDbContext context)
        {
            List<Event> events = new List<Event>()
            {
                new Event {EventFrom = new DateTime(2017,2,19,8,30,0),
                    EventTo =new DateTime(2017,2,19,10,30,0),
                    EnteredBy ="a",
                    CreationDate=System.DateTime.Now,
                    IsActive=true,
                    //ActivityId =context.Activities.Find("Leadership General Assembly Meeting").ActivityId 
                    ActivityId=context.Activities.Single(s=>s.ActivityDescription=="Leadership General Assembly Meeting").ActivityId },
                    //ActivityId=2
            

                    new Event {EventFrom = new DateTime(2017,2,20,8,30,0),
                    EventTo =new DateTime(2017,2,20,11,30,0),
                    EnteredBy ="a",
                    CreationDate=System.DateTime.Now,
                    IsActive=true,
                    ActivityId=context.Activities.Single(s=>s.ActivityDescription=="Youth Bowling Tournament").ActivityId  },

                    new Event        {
                    EventFrom = new DateTime(2017, 2, 27, 8, 30, 0),
                    EventTo = new DateTime(2017, 2, 27, 11, 30, 0),
                    EnteredBy = "a",
                    CreationDate = System.DateTime.Now,
                    IsActive = true,
                    ActivityId = context.Activities.Single(s => s.ActivityDescription == "Seniors Golf Tournament").ActivityId            },






        };
            return events;

        }
    }
}
