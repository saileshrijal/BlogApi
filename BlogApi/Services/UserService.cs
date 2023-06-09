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

    public async Task ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var user = await _userManager.FindByIdAsync(changePasswordDto.UserId!) ?? throw new Exception("User not found");
        var checkPassword = await _userManager.CheckPasswordAsync(user, changePasswordDto.OldPassword!);
        if (!checkPassword) throw new Exception("Old password is incorrect!");
        await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword!, changePasswordDto.NewPassword!);
    }

    public async Task ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userManager.FindByIdAsync(resetPasswordDto.UserId!) ?? throw new Exception("User not found");
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        await _userManager.ResetPasswordAsync(user, token, resetPasswordDto.Password!);
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
            Status = userDto.Status,
        };
        await _userManager.CreateAsync(applicationUser, userDto.Password!);
        await _userManager.AddToRoleAsync(applicationUser, userDto.UserRole!);
        tx.Complete();
        return applicationUser;
    }

    public async Task ToggleUserStatus(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId) ?? throw new Exception($"User with id {userId} not found");
        user.Status = !user.Status;
        await _userManager.UpdateAsync(user);
    }

    public async Task<ApplicationUser> Update(string id, UserDto userDto)
    {
        var user = await _userManager.FindByIdAsync(id) ?? throw new Exception("User not found");
        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        await _userManager.UpdateAsync(user);
        return user;
    }

    public async Task UpdateProfile(UpdateProfileDto updateProfileDto)
    {
        var user = await _userManager.FindByIdAsync(updateProfileDto.UserId!) ?? throw new Exception("User not found");
        user.FirstName = updateProfileDto.FirstName;
        user.LastName = updateProfileDto.LastName;
        await _userManager.UpdateAsync(user);
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