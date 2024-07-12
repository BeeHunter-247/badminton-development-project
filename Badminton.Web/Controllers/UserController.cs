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
        private readonly Dictionary<string, string> _otpStorage;
  

        public UserController(CourtSyncContext context, IOptionsMonitor<AppSetting> optionsMonitor, IMapper mapper, IEmailService emailService)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
            _mapper = mapper;
            _emailService = emailService;
            _otpStorage = new Dictionary<string, string>();
         
        }

        // Controller actions and logic...



        [HttpPost("Login")]
        //User/Admin
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
                    Message = "Account is not verified.Please verified your account"
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
                OtpExpiration = DateTime.UtcNow.AddMinutes(10) // Example: OTP expires in 10 minutes
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
                    return "Manager";
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
            return user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrator");
        }
    }

}