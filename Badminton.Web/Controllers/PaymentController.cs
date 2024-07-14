using Badminton.Web.DTO.Payment;
using Badminton.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Badminton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly MomoService _momoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentController(IHttpContextAccessor httpContextAccessor, MomoService momoService)
        {

            _momoService = momoService;
            _httpContextAccessor = httpContextAccessor;
        }

        //Create payment
        [HttpPost]
        public IActionResult CreatePayment([FromBody] PaymentDTO paymentDtos)
        {
            string? userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _momoService.CreatePayment(paymentDtos, userId);
            return Ok(result);
        }
    }
}
