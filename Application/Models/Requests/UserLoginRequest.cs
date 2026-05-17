using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{

    public class UserLoginRequest
    {
        [Required]
        public string? UserNameOrEmail { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}

