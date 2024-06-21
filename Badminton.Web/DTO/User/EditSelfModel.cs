using Badminton.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO.User
{
    public class EditSelfModel
    {

        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [PhoneNumber]
        public string Phone { get; set; }
    }
}
