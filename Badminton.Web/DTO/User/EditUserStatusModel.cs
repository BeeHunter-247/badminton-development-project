namespace Badminton.Web.DTO.User
{
    public class EditUserStatusModel
    {
        public string UserName { get; set; } // Tên người dùng cần chỉnh sửa UserStatus
        public int UserStatus { get; set; } // Giá trị UserStatus mới (0: bình thường, 1: bị ban)
    }
}