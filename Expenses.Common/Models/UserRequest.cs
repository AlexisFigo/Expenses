using System.ComponentModel.DataAnnotations;

namespace Expenses.Common.Models
{
    public class UserRequest
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}
