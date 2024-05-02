namespace BG.CampusLife.Infrastructure.Identity;

public static class IdentitySeed
{
    public static void SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.RoleExistsAsync("Admin").Result)
        {
            var role = new IdentityRole() { Name = "Admin", NormalizedName = "Admin".ToUpper() };
            roleManager.CreateAsync(role).Wait();
        }
        
        if (!roleManager.RoleExistsAsync("Faculty").Result)
        {
            var role = new IdentityRole() { Name = "Faculty", NormalizedName = "Faculty".ToUpper() };
            roleManager.CreateAsync(role).Wait();
        }

        if (!roleManager.RoleExistsAsync("Student").Result)
        {
            var role = new IdentityRole() { Name = "Student", NormalizedName = "Student".ToUpper() };
            roleManager.CreateAsync(role).Wait();
        }
        
        if (!roleManager.RoleExistsAsync("Explorer").Result)
        {
            var role = new IdentityRole() { Name = "Explorer", NormalizedName = "Explorer".ToUpper() };
            roleManager.CreateAsync(role).Wait();
        }

        if (userManager.FindByEmailAsync("Test@Email.com").Result is null)
        {
            var user = new ApplicationUser()
            {
                Email = "Test@Email.com",
                EmailConfirmed = true,
                IsActive = true,
                NormalizedEmail = "Test@Email.com".ToUpper(),
                NormalizedUserName = "Test@Email.com".ToUpper(),
                UserName = "Test@Email.com"
            };
            var result = userManager.CreateAsync
                (user, "Test#1234").Result;
            
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user,
                    "Admin").Wait();
            }
        }

        if (userManager.FindByEmailAsync("User@Email.com").Result is null)
        {
            var user = new ApplicationUser()
            {
                Email = "User@Email.com",
                EmailConfirmed = true,
                IsActive = true,
                NormalizedEmail = "User@Email.com".ToUpper(),
                NormalizedUserName = "User@Email.com".ToUpper(),
                UserName = "User@Email.com"
            };
            var result = userManager.CreateAsync
                (user, "Test#1234").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user,
                    "Student").Wait();
            }
        }
    }
}