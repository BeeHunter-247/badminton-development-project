using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO.User
{
    public class EditSelfModel
    {
        public string FullName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }
    }
}
