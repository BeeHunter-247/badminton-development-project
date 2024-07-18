using Badminton.Web.DTO.Payment.Vnpay.Response;
using Badminton.Web.DTO.Payment;
using Badminton.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Badminton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentVnpayController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly VnpayService _vnpayService;

        public PaymentVnpayController(IHttpContextAccessor httpContextAccessor, VnpayService vnpayService)
        {
            _httpContextAccessor = httpContextAccessor;
            _vnpayService = vnpayService;
        }
        //Create payment
        [HttpPost]
        public IActionResult CreatePayment([FromBody] PaymentDTO paymentDtos)
        {
            string? userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            string? ipAddress = GetClientIpAddress(_httpContextAccessor.HttpContext);
            var result = _vnpayService.CreatePayment(paymentDtos, userId, ipAddress);
            return Ok(result);
        }

        private string? GetClientIpAddress(HttpContext httpContext)
        {
            // Check for proxy headers
            string? ipAddress = httpContext?.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(ipAddress))
            {
                // Use the IP address from the proxy header
                return ipAddress;
            }
            else
            {
                // Use the local IP address
                return httpContext?.Connection?.LocalIpAddress?.ToString();
            }
        }
    }
}
