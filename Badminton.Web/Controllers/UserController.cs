using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.DTO.OTP;
using Badminton.Web.DTO.User;
using Badminton.Web.Models;
using Badminton.Web.Services.OTP;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Badminton.Web.DTO.Admin;

namespace Badminton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CourtSyncContext _context;
        private readonly AppSetting _appSettings;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        


        public UserController(CourtSyncContext context, IOptionsMonitor<AppSetting> optionsMonitor, IMapper mapper, IEmailService emailService)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
            _mapper = mapper;
            _emailService = emailService;


        }

        // Controller actions and logic...
        [HttpGet("GetTop5PeopleUseMostAmount")]
        public async Task<IActionResult> GetTop7PeopleUseMostAmount()
        {
            var topUsers = await _context.Bookings
                .Where(b => new[] { 1, 3, 4 }.Contains(b.Status)) // Lọc những Booking có Status là 1, 3, hoặc 4
                .GroupBy(b => new { b.UserId, b.User.UserName, b.User.Email })
                .Select(g => new TopUserDTO
                {
                    UserId = g.Key.UserId,
                    UserName = g.Key.UserName,
                    Email = g.Key.Email,
                    TotalAmount = g.Where(b => new[] { 1, 3, 4 }.Contains(b.Status)).Sum(b => b.Amount ?? 0) // Chỉ tính tổng Amount của những Booking có Status là 1, 3, hoặc 4
                })
                .OrderByDescending(u => u.TotalAmount)
                .Take(7)
                .ToListAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                Data = topUsers
            });
        }

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

            // Map each user to UserAdminDTO
            var userDtoList = new List<UserAdminDTO>();
            foreach (var user in users)
            {
                var userDto = _mapper.Map<UserAdminDTO>(user);

                // Convert RoleType from int to string (assuming GetUserRole is a method you have elsewhere)
                userDto.RoleType = GetUserRole(user.RoleType);

                userDtoList.Add(userDto);
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = userDtoList
            });
        }


        [HttpGet("GetTotalUserByRoleType3")]
        public async Task<IActionResult> GetTotalUserByRoleType3()
        {

            var roleType = 3; // Giá trị RoleType cần kiểm tra
            var totalUsers = await _context.Users.CountAsync(u => u.RoleType == roleType);

            return Ok(new ApiResponse
            {
                Success = true,
                Data = totalUsers
            });
        }



        // GetById
        //
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

            // Map the user to UserAdminDTO
            var userDto = _mapper.Map<UserAdminDTO>(user);

            // Convert the RoleType from int to string
            userDto.RoleType = GetUserRole(user.RoleType);

            return Ok(new ApiResponse
            {
                Success = true,
                Data = userDto
            });
        }


        [HttpGet("GetCurrentUser")]
        [Authorize]
        //User and Admin
        public async Task<IActionResult> GetCurrentUser()


        {
            // Log claims for debugging
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            // Get the user ID from the claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new ApiResponse
                {
                    Success = false,
                    Message = "User ID claim not found"
                });
            }

            // Parse the user ID
            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid user ID"
                });
            }

            // Retrieve the user from the database
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "User not found"
                });
            }

            // Map the user to a DTO
            var userDTO = _mapper.Map<UserDTO>(user);

            // Return the user details
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "User found",
                Data = userDTO
            });
        }
        [HttpGet("GetTotalUserInSystem")]
        public async Task<IActionResult> GetTotalUserInSystem()
        {
            var totalAdmin = await _context.Users.CountAsync(u => u.RoleType == 0);
            var totalOwner = await _context.Users.CountAsync(u => u.RoleType == 1);
            var totalUser = await _context.Users.CountAsync(u => u.RoleType == 3);

            var result = new TotalUserDTO
            {
                TotalAdmin = totalAdmin,
                TotalOwner = totalOwner,
                TotalUser = totalUser
            };

            return Ok(new ApiResponse
            {
                Success = true,
                Data = result
            });
        }

        [HttpGet("GetTotalCourtInSystem")]
        public async Task<IActionResult> GetTotalCourtInSystem()
        {
            var totalCourts = await _context.Courts
                .GroupBy(c => c.OwnerId)
                .Select(g => new TotalCourtDTO
                {
                    OwnerName = g.FirstOrDefault().Owner.FullName,
                    TotalCourt = g.Count()
                })
                .ToListAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                Data = totalCourts
            });
        }










        [HttpPost("Login")]
        // User/Admin
        public async Task<IActionResult> Validate(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var user = _context.Users.SingleOrDefault(p => p.UserName == model.UserName);
            if (user == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid Username/Password"
                });
            }

            bool passwordValid = false;

            // Check if the stored password is hashed
            if (user.Password.StartsWith("$2a$"))
            {
                // Password is hashed, verify using BCrypt
                passwordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            }
            else
            {
                // Password is plain text, compare directly
                passwordValid = (model.Password == user.Password);
            }

            if (!passwordValid)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid Username/Password"
                });
            }

            // Check if user's Verify status is 4
            if (user.Verify != 4)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Account is not verified. Please verify your account"
                    // Optionally, you can add more details in the response if needed
                });
            }

            // Check if user's UserStatus is 1 (bị ban)
            if (user.UserStatus == 1)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Account is locked. Please contact support for assistance."
                    // Optionally, you can add more details in the response if needed
                });
            }

            // Continue with authentication and claims generation
            var username = user.UserName;
            var fullname = user.FullName;
            var id = user.UserId;
            var email = user.Email;
            var phone = user.Phone;
            var role = GetUserRole(user.RoleType);
            if (role == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid Role"
                });
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, id.ToString()),
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Email, email),
        new Claim(ClaimTypes.MobilePhone, phone),
        new Claim("FullName", fullname),
        new Claim("Id", id.ToString()), // Ensure this claim is added
        new Claim(ClaimTypes.Role, role)
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(3)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            var token = GenerateToken(user);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Authentication successful",
                Data = new { Id = id, UserName = username, FullName = fullname, Email = email, Phone = phone, Role = role, Token = token }
            });
        }







        [HttpPost("registerwithotp")]
        public async Task<IActionResult> RegisterWithOtp(SendOtpModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
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

            var existingUserByUsername = await _context.Users.SingleOrDefaultAsync(u => u.UserName == model.Username);
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

            var existingUserByPhone = await _context.Users.SingleOrDefaultAsync(u => u.Phone == model.Phone);
            if (existingUserByPhone != null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Phone number already exists"
                });
            }

            // Generate random OTP
            var otp = new Random().Next(100000, 999999).ToString();

            // Hash OTP before saving to database
            var hashedOtp = BCrypt.Net.BCrypt.HashPassword(otp);

            var user = new User
            {
                UserName = model.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password), // Hashed password
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone,
                RoleType = 3, // Set RoleType to 3 for User
                Otp = hashedOtp, // Save hashed OTP
                OtpExpiration = DateTime.UtcNow.AddMinutes(5), // Example: OTP expires in 5 minutes
                Verify = 0, // Not verified yet
                AccountBalance = 1000000

            };

            _context.Users.Add(user);

            try
            {
                await _emailService.SendEmailAsync(model.Email, "Your OTP Code", $"Your OTP code is {otp}");
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Registration successful. OTP sent successfully."
                });
            }
            catch (DbUpdateException dbEx)
            {
                // Extract the inner exception details
                var innerException = dbEx.InnerException != null ? dbEx.InnerException.Message : dbEx.Message;
                return StatusCode(500, new ApiResponse { Success = false, Message = $"Failed to send OTP: {innerException}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { Success = false, Message = $"An unexpected error occurred: {ex.Message}" });
            }
        }



        [HttpPost("verifyotp")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid Email/OTP"
                });
            }

            // Compare hashed OTP input with hashed OTP stored in database and check expiration
            if (BCrypt.Net.BCrypt.Verify(model.Otp, user.Otp) && user.OtpExpiration > DateTime.UtcNow)
            {
                user.Verify = 4;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "OTP verified successfully"
                });
            }

            return Ok(new ApiResponse
            {
                Success = false,
                Message = "Invalid OTP or OTP has expired"
            });
        }


        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid Email"
                });
            }

            // Generate password reset token
            var token = Guid.NewGuid().ToString();
            user.PasswordResetToken = BCrypt.Net.BCrypt.HashPassword(token);
            user.PasswordResetTokenExpiration = DateTime.UtcNow.AddHours(1);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            var resetLink = $"http://localhost:5173/reset-password/{token}";

            try
            {
                await _emailService.SendEmailAsync(user.Email, "Reset Password", $"Click the link to reset your password: {resetLink}");
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Password reset link has been sent to your email."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { Success = false, Message = $"An error occurred: {ex.Message}" });
            }
        }




        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(SetNewPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Password and confirm password do not match"
                });
            }

            var user = await _context.Users
                .Where(u => u.PasswordResetTokenExpiration > DateTime.UtcNow)
                .ToListAsync();

            var matchedUser = user.SingleOrDefault(u => BCrypt.Net.BCrypt.Verify(model.Token, u.PasswordResetToken));

            if (matchedUser == null)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid or expired token"
                });
            }

            matchedUser.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            matchedUser.PasswordResetToken = null;
            matchedUser.PasswordResetTokenExpiration = null;

            _context.Users.Update(matchedUser);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Password has been reset successfully"
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
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == model.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.OldPassword, user.Password))//Hash pass
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid Username or Old Password. Please again"
                });
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Password changed successfully"
            });
        }

       
    
        //Edit Account (User)
        [HttpPut("EditSelf")]
        [Authorize]
        public async Task<IActionResult> EditSelf(EditSelfModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data"
                });
            }

            // Get the ID of the user making the request
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new ApiResponse
                {
                    Success = false,
                    Message = "User ID claim not found"
                });
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid user ID"
                });
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "User not found"
                });
            }

            var existingUserByEmail = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email && u.UserId != userId);
            if (existingUserByEmail != null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Email already exists"
                });
            }

            var existingUserByPhone = await _context.Users.SingleOrDefaultAsync(u => u.Phone == model.Phone && u.UserId != userId);
            if (existingUserByPhone != null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Phone number already exists"
                });
            }

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.Phone = model.Phone;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "User updated successfully"
            });
        }


        [HttpPut("EditUserStatus")]
        [Authorize]
        public async Task<IActionResult> EditUserStatus(EditUserStatusModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data"
                });
            }

            // Check if the user is an admin
            if (!IsAdmin(User))
            {
                return Unauthorized(new ApiResponse
                {
                    Success = false,
                    Message = "Access denied. Admins only."
                });
            }

            // Check if UserStatus is within the valid range
            if (model.UserStatus < 0 || model.UserStatus > 1)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid UserStatus"
                });
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == model.UserName);
            if (user == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "User not found"
                });
            }

            user.UserStatus = model.UserStatus;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "User status updated successfully"
            });
        }
        [HttpPut("EditRole")]
        [Authorize]
        public async Task<IActionResult> EditRole(EditRoleModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data"
                });
            }

            // Check if the user is an admin
            if (!IsAdmin(User))
            {
                return Unauthorized(new ApiResponse
                {
                    Success = false,
                    Message = "Access denied. Admins only."
                });
            }

            // Check if RoleType is within the valid range
            if (model.RoleType < 0 || model.RoleType > 3)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid role type"
                });
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == model.UserName);
            if (user == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "User not found"
                });
            }

            // Get the role name from roleType
            var roleName = GetUserRole(model.RoleType);
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid role type"
                });
            }

            user.RoleType = model.RoleType;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "User role updated successfully"
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
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.MobilePhone, user.Phone),
            new Claim("FullName", user.FullName),
            new Claim(ClaimTypes.Role, GetUserRole(user.RoleType))
        }),
                Expires = DateTime.UtcNow.AddHours(3),
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
                    return "Owner";
                case 2:
                    return "Staff";
                case 3:
                    return "User";
                default:
                    return null;
            }
        }
        private bool IsAdmin(ClaimsPrincipal user)
{
    // Cho phép cả role là "Administrator" và "Owner" đều được xem là Admin
    return user.Claims.Any(c => c.Type == ClaimTypes.Role && (c.Value == "Administrator" || c.Value == "Owner"));
}

    }

}