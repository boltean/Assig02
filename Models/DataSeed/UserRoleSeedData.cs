
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenithCore.Data;
using ZenithCore.Models;

namespace ZenithCore.Data.Seed.Users
{
    public static class UserRoleSeedData
    {
        public static async void SeedUsersAndRoles(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            //var context = app.ApplicationServices.GetService<ApplicationDbContext>();
            UserWithRoles[] usersWithRoles = {
                // Add a new user 'admin' with roles 'Administrator' and 'Patient' with password 'P@$$w0rd'
                new UserWithRoles("admin", new string[] { "Administrator" , "Patient" },"P@$$w0rd"),//user and optional roles and password you want to seed 

                // Add a new user 'guest', no roles and default password 'Open123$'
                new UserWithRoles("guest"),

                // Add a new user 'bob', role 'Patient' and default password 'Open123$'
                new UserWithRoles("jane",new string[]{"Patient" }) //seed roles to existing users (e.g. facebook login).
            };

            foreach (var u in usersWithRoles)
            {
                foreach (string role in u.Roles)
                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        var roleStore = new RoleStore<IdentityRole>(context);
                        var newRole = new IdentityRole { Name = role, NormalizedName = role.ToUpper() };
                        await roleStore.CreateAsync(newRole);
                    }
                var ExistingUser = context.Users.FirstOrDefault(p => p.NormalizedUserName == u.User.NormalizedUserName);
                if (ExistingUser == null) //the following syntax: !context.Users.FirstOrDefault(p => p.NormalizedUserName == userWithRoles.User.NormalizedUserName)) 
                                          //provokes execption:(ExecuteReader requires an open and available Connection.) 
                    await new UserStore<ApplicationUser>(context).CreateAsync(u.User);

                await app.AssignRoles(u); //assign also to existing users.
            }

            await context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRoles(this IApplicationBuilder app, UserWithRoles uWR)
        {
            UserManager<ApplicationUser> _userManager = app.ApplicationServices.GetService(typeof(UserManager<ApplicationUser>)) as UserManager<ApplicationUser>;

            ApplicationUser user = await _userManager.FindByNameAsync(uWR.User.NormalizedUserName);
            var result = await _userManager.AddToRolesAsync(user, uWR.Roles);
            return result;
        }
    }
}
