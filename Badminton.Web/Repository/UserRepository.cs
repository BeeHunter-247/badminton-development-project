using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Badminton.Web.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CourtSyncContext _context;

        public UserRepository(CourtSyncContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> UpdateAsync(User user)  // Thêm phương thức này
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
            if (existingUser == null)
            {
                return null; // hoặc có thể throw exception hoặc trả về kết quả phù hợp khác
            }

            // Cập nhật các thuộc tính cần thiết của người dùng
            existingUser.UserName = user.UserName;
            existingUser.FullName = user.FullName;
            existingUser.Password = user.Password;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            existingUser.RoleType = user.RoleType;
            existingUser.Otp = user.Otp;
            existingUser.OtpExpiration = user.OtpExpiration;
            existingUser.Verify = user.Verify;
            existingUser.UserStatus = user.UserStatus;
            existingUser.AccountBalance = user.AccountBalance;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return existingUser;
        }
    }
}