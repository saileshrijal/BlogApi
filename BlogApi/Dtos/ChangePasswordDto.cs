﻿namespace BlogApi.Dtos
{
    public class ChangePasswordDto
    {
        public string? UserId { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
