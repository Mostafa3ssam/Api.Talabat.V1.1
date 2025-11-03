using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.Identity;

namespace Talabat.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> _userManager)
        {
            if (!_userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "UncleTefa",
                    Email = "EngTefa@gmail.com",
                    UserName = "EngTefa",
                    PhoneNumber = "0112233445566"
                };
                await _userManager.CreateAsync(User, "Pa$$w0ord");

            }
            
        }
    }
}
