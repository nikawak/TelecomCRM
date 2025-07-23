using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelecomCRM.Infrastructure
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = new[] { "Admin", "Client", "Tech" };

            foreach (var role in roles)
            {
                bool exists = await roleManager.RoleExistsAsync(role);
                if (!exists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }

}
