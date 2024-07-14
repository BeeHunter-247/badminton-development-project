using Badminton.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Badminton.Web.DTO.Payment.Request
{
    [BindProperties]
    public class MomoOneTimePaymentResultRequest
    {
        public string? partnerCode { get; set; } = string.Empty;
        public string? orderId { get; set; } = string.Empty;
        public string? requestId { get; set; } = string.Empty;
        public long amount { get; set; }
        public string? orderInfo { get; set; } = string.Empty;
        public string? orderType { get; set; } = string.Empty;
        public string? transId { get; set; } = string.Empty;
        public string? message { get; set; } = string.Empty;
        public int resultCode { get; set; }
        public string? payType { get; set; } = string.Empty;
        public long responseTime { get; set; }
        public string? extraData { get; set; } = string.Empty;
        public string? signature { get; set; } = string.Empty;


    }
}