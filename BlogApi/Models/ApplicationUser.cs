using Microsoft.AspNetCore.Identity;

namespace BlogApi.Models;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedDate { get; set; }
}