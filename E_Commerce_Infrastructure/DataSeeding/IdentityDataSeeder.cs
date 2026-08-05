using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Common;
using E_Commerce_Domain.Contract;
using E_Commerce_Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Infrastructure.DataSeeding
{
    public class IdentityDataSeeder : IDataSeeder
    {
        private readonly StoreIdentityDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityDataSeeder(StoreIdentityDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)

        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task SeedDataAsync(CancellationToken ct = default)
        {

            var pendingmigraions = await _dbContext.Database.GetPendingMigrationsAsync(ct);
            if (pendingmigraions.Any())
            {
                await _dbContext.Database.MigrateAsync(ct);
            }

            if (!await _roleManager.Roles.AnyAsync(ct))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            if (!await _userManager.Users.AnyAsync(ct))
            {

                var admin = new ApplicationUser()
                {
                    DisplayName = "Mohamed Ahmed",
                    Email = "Mohamed@gmail.com",
                    UserName = "Mohamed",
                    PhoneNumber = "011235B669"

                };

                var result = await _userManager.CreateAsync(admin, "P@ssw0rd");

                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(admin, "Admin");
                else
                    Console.WriteLine("Error happen in seed");
            }

        }
    }
}
