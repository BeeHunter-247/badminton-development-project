using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.DTO.User
{
    public class EditRoleModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [Range(0, 3, ErrorMessage = "RoleType must be between 0 and 3")]
        public int RoleType { get; set; }
    }

}
