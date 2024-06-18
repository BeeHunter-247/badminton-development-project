using Badminton.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CourtSyncContext _context;
        private readonly AppSetting _appSettings;

        public UserController(CourtSyncContext context, IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }

        [HttpPost("Login")]
        public IActionResult Validate(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data"
                    
                });
            }

            var user = _context.Users.SingleOrDefault(p => p.Username == model.UserName && p.Password == model.Password);
            if (user == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid Username/Password"
                });
            }

            var role = GetUserRole(user.RoleType); // Lấy vai trò dựa trên RoleType
            if (role == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid Role"
                });
            }

            var token = GenerateToken(user); // Tạo token
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Authentication successful",
                Data = new { Role = role, Token = token } // Trả về vai trò và token của người dùng
            });
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data"
                });
            }

            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Password and confirm password do not match"
                });
            }

            var existingUserByUsername = await _context.Users.SingleOrDefaultAsync(u => u.Username == model.Username);
            if (existingUserByUsername != null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Username already exists"
                });
            }

            var existingUserByEmail = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            if (existingUserByEmail != null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Email already exists"
                });
            }

            var user = new User
            {
                Username = model.Username,
                Password = model.Password, // You should hash the password before storing it
                Email = model.Email,
                Phone = model.Phone,
                RoleType = 2 // Set RoleType to 2 for Investor
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Registration successful"
            });
        }




        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data"
                });
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == model.Username);
            if (user == null || user.Password != model.OldPassword)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid Username or Old Password"
                });
            }

            user.Password = model.NewPassword; // You should hash the password before storing it
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Password changed successfully"
            });
        }

        //Get All
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!IsAdmin(User))
            {
                return Unauthorized(new ApiResponse
                {
                    Success = false,
                    Message = "Unauthorized"
                });
            }

            var users = await _context.Users.ToListAsync();
            return Ok(new ApiResponse
            {
                Success = true,
                Data = users
            });
        }

        //GetById
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (!IsAdmin(User))
            {
                return Unauthorized(new ApiResponse
                {
                    Success = false,
                    Message = "Unauthorized"
                });
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "User not found"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = user
            });
        }
        //Edi Acoount
        [HttpPut("EditUser/{id}")]
        public async Task<IActionResult> EditUser(int id, EditUserModel model)
        {
            if (!IsAdmin(User))
            {
                return Unauthorized(new ApiResponse
                {
                    Success = false,
                    Message = "Unauthorized"
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data"
                });
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "User not found"
                });
            }

            user.Email = model.Email;
            user.Phone = model.Phone;
            user.RoleType = model.RoleType;
            // Update other fields as needed

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "User updated successfully"
            });
        }
        //Delete Acoount
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!IsAdmin(User))
            {
                return Unauthorized(new ApiResponse
                {
                    Success = false,
                    Message = "Unauthorized"
                });
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "User not found"
                });
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "User deleted successfully"
            });
        }



        private string GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.MobilePhone, user.Phone),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("UserName", user.Username),
            new Claim("Id", user.UserId.ToString()),
            new Claim("TokenId", Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, GetUserRole(user.RoleType)) // Add role to claims
        }),
                Expires = DateTime.UtcNow.AddMinutes(60), // Extend token expiration as needed
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        private string GetUserRole(int roleType)
        {
            switch (roleType)
            {
                case 0:
                    return "Administrator";
                case 1:
                    return "Investor";
                case 2:
                    return "User";
                default:
                    return null;
            }
        }
        private bool IsAdmin(ClaimsPrincipal user)
        {
            return user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrator");
        }



    }
}
