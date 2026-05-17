using Domain.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests
{
    public class AddClientDTO
    {
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "First Name must be between 3 and 20 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Last Name must be between 3 and 20 characters.")]
        public string? LastName { get; set; }

        //[RegularExpression(@"^\d{8,9}$", ErrorMessage = "DNI number must be 8 or 9 digits.")]
        //[Required(ErrorMessage = "DNI number is required")]
        //public string? DniNumber { get; set; }

        //[Required(ErrorMessage = "Address  is required")]
        //[StringLength(30, MinimumLength = 3, ErrorMessage = "Address must be between 3 and 20 characters.")]
        //public string? Address { get; set; }

        [Required(ErrorMessage = "Username  is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters.")]
        public string? UserName { get; set; }

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
        [EmailAddress]
        [Required(ErrorMessage = "Email  is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password  is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Password must be at least 3 characters long.")]
        public string? Password { get; set; }

        [Required]
        public string UserType { get; set; }



    }
}
