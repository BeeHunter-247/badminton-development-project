using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO.User
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
