using BlogApi.Constants;
using BlogApi.Dtos;
using BlogApi.Models;
using BlogApi.Seeder.Interface;
using BlogApi.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace BlogApi.Seeder;

public class UserSeeder : IUserSeeder
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserService _userService;

    public UserSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserService userService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userService = userService;
    }

    public async Task SeedAdminUser()
    {
        var adminUser = await _userManager.GetUsersInRoleAsync(UserRole.Admin);
        if (!adminUser.Any())
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
            await _roleManager.CreateAsync(new IdentityRole(UserRole.User));

            var userDto = new UserDto()
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                FirstName = "Super",
                LastName = "Admin",
                UserRole = UserRole.Admin,
                Password = "Admin@0011"
            };
            await _userService.Create(userDto);
        }
        else
        {
            throw new Exception("Admin user already exists");
        }
    }
}