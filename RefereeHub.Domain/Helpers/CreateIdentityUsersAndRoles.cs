using Microsoft.AspNetCore.Identity;

namespace RefereeHub.Domain.Helpers;

public abstract class CreateIdentityUsersAndRoles
{
     public static async Task Create(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
     {
          await CreateUsersAndRoles(userManager, roleManager);
     }
     
     private static async Task CreateUsersAndRoles(UserManager<IdentityUser> userManager,
          RoleManager<IdentityRole> roleManager)
     {
          await CreateRole(roleManager, Roles.User);
          await CreateRole(roleManager, Roles.Admin);
          await CreateUser(userManager, "admin", Roles.Admin);
          await CreateUser(userManager, "testuser");
     }

     private static async Task CreateUser(UserManager<IdentityUser> userManager, string userName,
          string role = Roles.User)
     {
          if (await userManager.FindByEmailAsync(userName) == null)
          {
               var user = new IdentityUser
               {
                    UserName = userName,
                    Email = userName
               };

               var result = await userManager.CreateAsync(user, "Pa$$w0rd");

               if (result.Succeeded) await userManager.AddToRoleAsync(user, role);
          }
     }

     private static async Task CreateRole(RoleManager<IdentityRole> roleManager, string roleName)
     {
          if (!await roleManager.RoleExistsAsync(roleName))
          {
               var role = new IdentityRole
               {
                    Name = roleName
               };

               await roleManager.CreateAsync(role);
          }
     }
}