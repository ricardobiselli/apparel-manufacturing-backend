namespace Application.Models;

public class AuthenticationResponseDTO
{
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool MustChangePassword { get; set; }
}

