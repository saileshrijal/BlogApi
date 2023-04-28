namespace BlogApi.Result
{
    public class AuthResult
    {
        public string? Token { get; set; }
        public bool Success { get; set; }
        public List<string>? Errors { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; }
    }
}
