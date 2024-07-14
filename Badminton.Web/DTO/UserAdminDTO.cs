namespace Badminton.Web.DTO
{
    public class UserAdminDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoleType { get; set; } // Change from int to string
        public string UserStatus { get; set; }
    }
}
