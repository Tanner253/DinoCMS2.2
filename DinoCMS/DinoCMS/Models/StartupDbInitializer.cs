﻿using DinoCMS.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinoCMS.Models
{
    public class StartupDbInitializer 
    {
        private static List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole{Name = ApplicationRoles.Admin, NormalizedName = ApplicationRoles.Admin.ToUpper()},
             new IdentityRole{Name = ApplicationRoles.Member, NormalizedName = ApplicationRoles.Member.ToUpper()}
        };
        public static void SeedData(IServiceProvider servicesProvider, UserManager<ApplicationUser> userManager)
        {
            using (var dbContext = new UserDbContext(servicesProvider.GetRequiredService<DbContextOptions<UserDbContext>>()))
            {
               
               
                dbContext.Database.EnsureCreated();
                AddRoles(dbContext);
                //SeedUsers(userManager);
            }
        }
        /// <summary>
        /// seeds roles
        /// </summary>
        /// <param name="context"></param>
        private static void AddRoles(UserDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }
            foreach (var role in Roles)
            {
                context.Roles.Add(role);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// seeds a user 
        /// </summary>
        /// <param name="userManager"></param>
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync
                        ("user1").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "ADMIN";
                user.Email = "ADMIN@gmail.com";
                user.FirstName = "PrehistoricRealism";
                user.LastName = "STAFF";
                user.Birthday = new DateTime(2000, 3, 8);

                IdentityResult result = userManager.CreateAsync
                (user, "PrStaff10$").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }


        //    if (userManager.FindByNameAsync
        //("user2").Result == null)
        //    {
        //        ApplicationUser user = new ApplicationUser();
        //        user.UserName = "user2";
        //        user.Email = "user2@localhost";
        //        user.FullName = "Mark Smith";
        //        user.BirthDate = new DateTime(1965, 1, 1);

        //        IdentityResult result = userManager.CreateAsync
        //        (user, "password_goes_here").Result;

        //        if (result.Succeeded)
        //        {
        //            userManager.AddToRoleAsync(user,
        //                                "Administrator").Wait();
        //        }
        //    }
        }
    }
}
