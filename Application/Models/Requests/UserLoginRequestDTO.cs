using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests;

public class UserLoginRequestDTO
{
    [Required]
    public string EmployeeNumber { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}