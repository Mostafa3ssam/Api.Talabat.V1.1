using System.ComponentModel.DataAnnotations;

namespace Api.Talabat.V1.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
       
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(
    "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^A-Za-z\\d]).{8,}$",
    ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase, 1 digit, 1 special character, and be at least 8 characters long")]

        public string Password { get; set; }
    }
}
