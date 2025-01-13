using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Shortly.Domain.Entities.Identity;
using Shortly.Persistence.Constants;

namespace Shortly.Persistence.DataSeeder
{
    public static class DbSeeder
    {
        public static async Task SeedData(IApplicationBuilder applicationBuilder)
        {
            var scope = applicationBuilder.ApplicationServices.CreateScope();
            Log.Logger.Information("Start seeding data");
            try
            {
                // rosolve other dependencies
                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();

                // check if user is exit prevent duplicate seeding
                if(userManager.Users.Any() == false)
                {
                    var user = new ApplicationUser
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserStatus = Domain.Enums.UserStatus.Active,
                        AccountStatus = Domain.Enums.AccountStatus.Anonymous,
                        CreatedDate = DateTimeOffset.Now,
                        CreatedBy = Guid.NewGuid(),
                        PhoneNumberConfirmed = true,
                        TwoFactorEnabled = true,
                        LockoutEnabled = true,
                        AccessFailedCount = 5
                    };

                    if ((await roleManager.RoleExistsAsync(Roles.Admin)) == false)
                    {
                        Log.Logger.Information("Role administration is creating");
                        var roleResult = await roleManager.CreateAsync(new ApplicationRole(Roles.Admin)
                        {
                            Description = "role Admin",
                            RoleCode = "ADMIN"
                        });

                        if(roleResult.Succeeded == false)
                        {
                            var roleError = roleResult.Errors.Select(x => x.Description);
                            Log.Logger.Error($"Failed to create admin role. Detail {string.Join(",",roleError)}");

                            return;
                        }

                        Log.Logger.Information("Role administration created successfully");
                    }

                    // Attempt to create admin user
                    var createUserResult = await userManager.CreateAsync(user: user, password: "Admin@123");

                    if(createUserResult.Succeeded == false)
                    {
                        var errors = createUserResult.Errors.Select(x => x.Description);
                        Log.Logger.Error($"Failed to create admin role. Detail {string.Join(",", errors)}");

                        return;
                    }

                    // adding role to user
                    var addUserToRoleResult = await userManager
                                    .AddToRoleAsync(user: user, role: Roles.Admin);

                    if (addUserToRoleResult.Succeeded == false)
                    {
                        var errors = addUserToRoleResult.Errors.Select(e => e.Description);
                        Log.Logger.Error($"Failed to add admin role to user. Errors : {string.Join(",", errors)}");
                    }
                    Log.Logger.Information("Admin user is created");
                }
            }
            catch(Exception ex)
            {
                Log.Logger.Error("Something went wrong", ex.Message);
            }
        }
    }
}
