using AutoMapper;
using Badminton.Web.DTO.Payment.Request;
using Badminton.Web.DTO.Payment.Response;
using Badminton.Web.DTO.Payment;
using Badminton.Web.Models;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using Badminton.Web.VnPay.Config;
using TestVnPay.VnPay.Lib;
using Badminton.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Badminton.Web.Services
{
    public class VnPayService
    {
        private VnpayPayResponse _vnpayPayResponse;
        private readonly VnpayConfig _vnpayConfig;
        private VnpayPayRequest _vnpayPayRequest;
        private readonly CourtSyncContext _context;
        private readonly IMapper _mapper;
   

        public VnPayService(VnpayPayResponse vnpayPayResponse, IOptions<VnpayConfig> vnpayConfig, VnpayPayRequest vnpayPayRequest, CourtSyncContext context, IMapper mapper)
        {
            _vnpayPayResponse = vnpayPayResponse;
            _vnpayConfig = vnpayConfig.Value;
            _vnpayPayRequest = vnpayPayRequest;
            _context = context;
            _mapper = mapper;
   
        }

        public SortedList<string, string> responseData
           = new SortedList<string, string>(new VnpayCompare());

        //hàm này để sắp các string theo thứ tự trừ trên xuống theo bảng chữ cái
        public SortedList<string, string> requestData
            = new SortedList<string, string>(new VnpayCompare());

        //Tạo link thanh toán VNPAY
        public string GetLink(string baseUrl, string secretKey)
        {
            MakeRequestData();
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in requestData)
            {
                if (!String.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }

            string result = baseUrl + "?" + data.ToString();
            var secureHash = HashHelper.HmacSHA512(secretKey, data.ToString().Remove(data.Length - 1, 1));
            return result += "vnp_SecureHash=" + secureHash;
        }

        //Check data if it is not null then add to requestData
        public void MakeRequestData()
        {
            if (_vnpayPayRequest.vnp_Amount != null)
                requestData.Add("vnp_Amount", _vnpayPayRequest.vnp_Amount.ToString() ?? string.Empty);
            if (_vnpayPayRequest.vnp_Command != null)
                requestData.Add("vnp_Command", _vnpayPayRequest.vnp_Command);
            if (_vnpayPayRequest.vnp_CreateDate != null)
                requestData.Add("vnp_CreateDate", _vnpayPayRequest.vnp_CreateDate);
            if (_vnpayPayRequest.vnp_CurrCode != null)
                requestData.Add("vnp_CurrCode", _vnpayPayRequest.vnp_CurrCode);
            if (_vnpayPayRequest.vnp_BankCode != null)
                requestData.Add("vnp_BankCode", _vnpayPayRequest.vnp_BankCode);
            if (_vnpayPayRequest.vnp_IpAddr != null)
                requestData.Add("vnp_IpAddr", _vnpayPayRequest.vnp_IpAddr);
            if (_vnpayPayRequest.vnp_Locale != null)
                requestData.Add("vnp_Locale", _vnpayPayRequest.vnp_Locale);
            if (_vnpayPayRequest.vnp_OrderInfo != null)
                requestData.Add("vnp_OrderInfo", _vnpayPayRequest.vnp_OrderInfo);
            if (_vnpayPayRequest.vnp_OrderType != null)
                requestData.Add("vnp_OrderType", _vnpayPayRequest.vnp_OrderType);
            if (_vnpayPayRequest.vnp_ReturnUrl != null)
                requestData.Add("vnp_ReturnUrl", _vnpayPayRequest.vnp_ReturnUrl);
            if (_vnpayPayRequest.vnp_TmnCode != null)
                requestData.Add("vnp_TmnCode", _vnpayPayRequest.vnp_TmnCode);
            if (_vnpayPayRequest.vnp_ExpireDate != null)
                requestData.Add("vnp_ExpireDate", _vnpayPayRequest.vnp_ExpireDate);
            if (_vnpayPayRequest.vnp_TxnRef != null)
                requestData.Add("vnp_TxnRef", _vnpayPayRequest.vnp_TxnRef);
            if (_vnpayPayRequest.vnp_Version != null)
                requestData.Add("vnp_Version", _vnpayPayRequest.vnp_Version);
        }



        //Check Signature response from VNPAY
        public bool IsValidSignature(string secretKey)
        {
            MakeResponseData();
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in responseData)
            {
                if (!String.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }
            string checkSum = HashHelper.HmacSHA512(secretKey,
                data.ToString().Remove(data.Length - 1, 1));
            return checkSum.Equals(_vnpayPayResponse.vnp_SecureHash, StringComparison.InvariantCultureIgnoreCase);
        }

        public void MakeResponseData()
        {
            if (_vnpayPayResponse.vnp_Amount != null)
                responseData.Add("vnp_Amount", _vnpayPayResponse.vnp_Amount.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_TmnCode))
                responseData.Add("vnp_TmnCode", _vnpayPayResponse.vnp_TmnCode.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_BankCode))
                responseData.Add("vnp_BankCode", _vnpayPayResponse.vnp_BankCode.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_BankTranNo))
                responseData.Add("vnp_BankTranNo", _vnpayPayResponse.vnp_BankTranNo.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_CardType))
                responseData.Add("vnp_CardType", _vnpayPayResponse.vnp_CardType.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_OrderInfo))
                responseData.Add("vnp_OrderInfo", _vnpayPayResponse.vnp_OrderInfo.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_TransactionNo))
                responseData.Add("vnp_TransactionNo", _vnpayPayResponse.vnp_TransactionNo.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_TransactionStatus))
                responseData.Add("vnp_TransactionStatus", _vnpayPayResponse.vnp_TransactionStatus.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_TxnRef))
                responseData.Add("vnp_TxnRef", _vnpayPayResponse.vnp_TxnRef.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_PayDate))
                responseData.Add("vnp_PayDate", _vnpayPayResponse.vnp_PayDate.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_ResponseCode))
                responseData.Add("vnp_ResponseCode", _vnpayPayResponse.vnp_ResponseCode ?? string.Empty);
        }

        //Create payment (save PaymentDtos to Database with Mapper)
        public string CreatePayment(PaymentDTO paymentDtos, string? IpAddress, string? UserId)
        {
            try
            {
                var payment = _mapper.Map<Payment>(paymentDtos);
                payment.PaymentStatus = 0; // Initialize payment status as required

                // Add the payment entity to the context
                var resultPayment = _context.Payments.Add(payment);

                // Save changes to the database
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    // Successfully saved payment, proceed with VNPAY request
                    _vnpayPayRequest = new VnpayPayRequest(
                        _vnpayConfig.Version,
                        _vnpayConfig.TmnCode,
                        DateTime.Now,
                        IpAddress ?? "127.0.0.1", // Use provided IP address or fallback
                        paymentDtos.RequiredAmount ?? 0,
                        paymentDtos.PaymentCurrency ?? string.Empty,
                        "other", // Adjust this as per your requirement
                        paymentDtos.PaymentContent ?? string.Empty,
                        _vnpayConfig.ReturnUrl,
                        resultPayment.Entity.PaymentId.ToString() // Use the generated PaymentId
                    );

                    // Generate the payment URL with secure hash
                    var paymentUrl = GetLink(_vnpayConfig.PaymentUrl, _vnpayConfig.HashSecret);

                    return paymentUrl; // Return the generated payment URL
                }
                else
                {
                    return "Lỗi rồi"; // Handle the case where payment save fails
                }
            }
            catch (DbUpdateException ex)
            {
                // Log or handle the database update exception
                var innerException = ex.InnerException;
                throw new Exception("Đã xảy ra lỗi khi lưu thanh toán. Vui lòng thử lại sau.", ex);
            }
        }





        //Check payment response from VNPAY
        public string CheckPaymentResponse(VnpayPayResponse vnpayPayResponse)
        {
            if (vnpayPayResponse != null)
            {
                _vnpayPayResponse = vnpayPayResponse;
                if (IsValidSignature(_vnpayConfig.HashSecret))
                {
                    var payment = _context.Payments.Find(int.Parse(_vnpayPayResponse.vnp_TxnRef));

                    if (payment != null)
                    {
                        if (payment.TotalPrice == (vnpayPayResponse.vnp_Amount / 100))
                        {
                            if (payment.PaymentStatus == 0) // Sử dụng số nguyên thay vì chuỗi
                            {
                                if (vnpayPayResponse.vnp_ResponseCode == "00" && vnpayPayResponse.vnp_TransactionStatus == "00")
                                {
                                    payment.PaymentStatus = 1;
                                    _context.Payments.Update(payment);
                                    var result = _context.SaveChanges();
                                    return "Confirm Success"; // "RspCode":"00"
                                }
                                else
                                {
                                    payment.PaymentStatus = 2;
                                    _context.Payments.Update(payment);
                                    var result = _context.SaveChanges();
                                    return "Có lỗi xảy ra trong quá trình xử lý";
                                }
                            }
                            else
                            {
                                return "Payment already confirmed"; // "RspCode":"02"
                            }
                        }
                        else
                        {
                            return "Invalid amount"; // "RspCode":"04"
                        }
                    }
                    else
                    {
                        return "Payment not found"; // "RspCode":"01"
                    }
                }
                else
                {
                    return "Invalid signature"; // "RspCode":"97"
                }
            }
            else
            {
                return "Input data required"; // "RspCode":"99"
            }
        }

    }
}

