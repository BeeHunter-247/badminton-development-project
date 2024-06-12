using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
