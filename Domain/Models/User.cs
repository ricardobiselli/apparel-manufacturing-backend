using Domain.Enums;

namespace Domain.Models;

public class User
{
    public int UserId { get; set; }
    public string EmployeeIdNumber { get; set; } = string.Empty;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; }
    public bool MustChangePassword { get; set; }
    public EntitiesState State { get; set; } = EntitiesState.Active;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }

    public User()
    {
        MustChangePassword = true;
    }
}


