using BlogApi.Dtos;
using BlogApi.Models;

namespace BlogApi.Services.Interface;

public interface IUserService
{
    Task<ApplicationUser> Create(UserDto userDto);
    Task<ApplicationUser> Update(string id, UserDto userDto);
    Task ToggleUserStatus(string userId);
    Task ChangePassword(ChangePasswordDto changePasswordDto);
    Task ResetPassword(ResetPasswordDto resetPasswordDto);
    Task UpdateProfile(UpdateProfileDto updateProfileDto);
}