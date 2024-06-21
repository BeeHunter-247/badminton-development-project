using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.Models
{
    public class EditUserModel
    {
        public string FullName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [PhoneNumber]
        public string Phone { get; set; }

        [Range(0, 3, ErrorMessage = "RoleType must be between 0 and 3," +
                                       "0:Admin" +
                                       "1:Manager" +
                                       "2:Staff" +
                                       "3:User")]
        public int RoleType { get; set; }
    }
}
