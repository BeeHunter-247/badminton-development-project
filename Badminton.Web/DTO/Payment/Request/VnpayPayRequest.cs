using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestVnPay.VnPay.Lib;

namespace Badminton.Web.DTO.Payment.Request
{
    public class VnpayPayRequest
    {
        public VnpayPayRequest() { }
        public VnpayPayRequest(string version, string tmnCode, DateTime createDate, string ipAddress,
            decimal amount, string currCode, string orderType, string orderInfo,
            string returnUrl, string txnRef)
        {
            vnp_Locale = "vn";
            vnp_IpAddr = ipAddress;
            vnp_Version = version;
            vnp_CurrCode = currCode;
            vnp_CreateDate = createDate.ToString("yyyyMMddHHmmss");
            vnp_TmnCode = tmnCode;
            vnp_Amount = (int)amount * 100;
            vnp_Command = "pay";
            vnp_OrderType = orderType;
            vnp_OrderInfo = orderInfo;
            vnp_ReturnUrl = returnUrl;
            vnp_TxnRef = txnRef;
        }

        public decimal? vnp_Amount { get; set; }
        public string? vnp_Command { get; set; }
        public string? vnp_CreateDate { get; set; }
        public string? vnp_CurrCode { get; set; }
        public string? vnp_BankCode { get; set; }
        public string? vnp_IpAddr { get; set; }
        public string? vnp_Locale { get; set; }
        public string? vnp_OrderInfo { get; set; }
        public string? vnp_OrderType { get; set; }
        public string? vnp_ReturnUrl { get; set; }
        public string? vnp_TmnCode { get; set; }
        public string? vnp_ExpireDate { get; set; }
        public string? vnp_TxnRef { get; set; }
        public string? vnp_Version { get; set; }
        public string? vnp_SecureHash { get; set; }
    }
}
