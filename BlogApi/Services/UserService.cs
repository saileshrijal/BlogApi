namespace BlogApi.Services;

using System.Threading.Tasks;
using System.Transactions;
using BlogApi.Dtos;
using BlogApi.Models;
using BlogApi.Services.Interface;
using Microsoft.AspNetCore.Identity;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser> Create(UserDto userDto)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await Validate(userDto.UserName, userDto.Email);
        var applicationUser = new ApplicationUser()
        {
            UserName = userDto.UserName,
            Email = userDto.Email,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            CreatedDate = DateTime.UtcNow,
        };
        await _userManager.CreateAsync(applicationUser, userDto.Password!);
        await _userManager.AddToRoleAsync(applicationUser, userDto.UserRole!);
        tx.Complete();
        return applicationUser;
    }

    private async Task Validate(string? username, string? email)
    {
        if (await _userManager.FindByNameAsync(username!) != null)
        {
            throw new Exception($"Username {username} already taken. Please use another username");
        }
        if (await _userManager.FindByEmailAsync(email!) != null)
        {
            throw new Exception($"Email {email} already taken. Please use another email");
        }
    }
}