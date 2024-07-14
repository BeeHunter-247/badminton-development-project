using AutoMapper;
using Badminton.Web.DTO.Payment.Request;
using Badminton.Web.DTO.Payment.Responese;
using Badminton.Web.Models;
using Badminton.Web.Momo.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using Badminton.Web.Helpers;
using Badminton.Web.DTO.Payment;
using Badminton.Web.Momo.Base;
using System.Net.Http;


namespace Badminton.Web.Services
{
    public class MomoService
    {
        private MomoOneTimePaymentCreateLinkResponse _momoPayResponse;
        private MomoOneTimePaymentResultRequest _momoResultRequest;
        private readonly MomoConfig _momoConfig;
        private MomoOneTimePaymentRequest _momoPayRequest;
        private readonly CourtSyncContext _context;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;


        public MomoService(HttpClient httpClient, MomoOneTimePaymentCreateLinkResponse momoPayResponse, IOptions<MomoConfig> momoConfig, CourtSyncContext context, IMapper mapper)
        {
            _momoPayResponse = momoPayResponse;
            _momoConfig = momoConfig.Value;
            _context = context;
            _mapper = mapper;
            _httpClient = httpClient;
        }


        //GetLink Momo
        public (bool, string?) GetLink(string paymentUrl)
        {
            var requestData = JsonConvert.SerializeObject(_momoPayRequest, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
            });
            var requestContent = new StringContent(requestData, Encoding.UTF8, "application/json");

            var createPaymentLinkRes = _httpClient.PostAsync(paymentUrl, requestContent).Result;

            if (createPaymentLinkRes.IsSuccessStatusCode)
            {
                var responseContent = createPaymentLinkRes.Content.ReadAsStringAsync().Result;
                var responseData = JsonConvert.DeserializeObject<MomoOneTimePaymentCreateLinkResponse>(responseContent);
                if (responseData.resultCode == "0")
                {
                    return (true, responseData.payUrl);
                }
                else
                {
                    return (false, responseData.message);
                }
            }
            else
            {
                return (false, createPaymentLinkRes.ReasonPhrase);
            }
        }

        public void MakeSignature(string accessKey, string secretKey)
        {
            var rawHash = $"accessKey={accessKey}" +
                $"&amount={_momoPayRequest.amount}" +
                $"&extraData={_momoPayRequest.extraData}" +
                $"&ipnUrl={_momoPayRequest.ipnUrl}" +
                $"&orderId={_momoPayRequest.orderId}" +
                $"&orderInfo={_momoPayRequest.orderInfo}" +
                $"&partnerCode={_momoPayRequest.partnerCode}" +
                $"&redirectUrl={_momoPayRequest.redirectUrl}" +
                $"&requestId={_momoPayRequest.requestId}" +
                $"&requestType={_momoPayRequest.requestType}";

            _momoPayRequest.signature = HashHelper.HmacSHA256(rawHash, secretKey);
        }

        public bool IsValidSignature(string accessKey, string secretKey)
        {
            var rawHash = $"accessKey={accessKey}" +
                   $"&amount={_momoResultRequest.amount}" +
                   $"&extraData={_momoResultRequest.extraData}" +
                   $"&message={_momoResultRequest.message}" +
                   $"&orderId={_momoResultRequest.orderId}" +
                   $"&orderInfo={_momoResultRequest.orderInfo}" +
                   $"&orderType={_momoResultRequest.orderType}" +
                   $"&partnerCode={_momoResultRequest.partnerCode}" +
                   $"&payType={_momoResultRequest.payType}" +
                   $"&requestId={_momoResultRequest.requestId}" +
                   $"&responseTime={_momoResultRequest.responseTime}" +
                   $"&resultCode={_momoResultRequest.resultCode}" +
                   $"&transId={_momoResultRequest.transId}";

            var checkSignature = HashHelper.HmacSHA256(rawHash, secretKey);
            return _momoResultRequest.signature.Equals(checkSignature);
        }

        public string CreatePayment(PaymentDTO paymentDto, string? userId)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            payment.PaymentStatus = "0";
            _context.Payments.Add(payment);
            _context.SaveChanges();

            var momoPayRequest = new MomoOneTimePaymentRequest(
                _momoConfig.PartnerCode,
                payment.PaymentId.ToString() ?? string.Empty,
                (long)paymentDto.RequiredAmount!,
                payment.PaymentId.ToString() ?? string.Empty,
                paymentDto.PaymentContent ?? string.Empty,
                _momoConfig.ReturnUrl,
                _momoConfig.IpnUrl,
                "captureWallet",
                string.Empty
            );

            MakeSignature(momoPayRequest, _momoConfig.AccessKey, _momoConfig.SecretKey);

            (bool createMomoLinkResult, string? createMessage) = GetLink(momoPayRequest, _momoConfig.PaymentUrl);

            if (createMomoLinkResult)
            {
                return createMessage ?? string.Empty;
            }
            else
            {
                return createMessage ?? "Lỗi khi tạo liên kết thanh toán Momo";
            }
        }

        public void MakeSignature(MomoOneTimePaymentRequest momoPayRequest, string accessKey, string secretKey)
        {
            var rawHash = $"accessKey={accessKey}" +
                $"&amount={momoPayRequest.amount}" +
                $"&extraData={momoPayRequest.extraData}" +
                $"&ipnUrl={momoPayRequest.ipnUrl}" +
                $"&orderId={momoPayRequest.orderId}" +
                $"&orderInfo={momoPayRequest.orderInfo}" +
                $"&partnerCode={momoPayRequest.partnerCode}" +
                $"&redirectUrl={momoPayRequest.redirectUrl}" +
                $"&requestId={momoPayRequest.requestId}" +
                $"&requestType={momoPayRequest.requestType}";

            momoPayRequest.signature = HashHelper.HmacSHA256(rawHash, secretKey);
        }

        public (bool, string?) GetLink(MomoOneTimePaymentRequest momoPayRequest, string paymentUrl)
        {
            var requestData = JsonConvert.SerializeObject(momoPayRequest, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
            });
            var requestContent = new StringContent(requestData, Encoding.UTF8, "application/json");

            var createPaymentLinkRes = _httpClient.PostAsync(paymentUrl, requestContent).Result;

            if (createPaymentLinkRes.IsSuccessStatusCode)
            {
                var responseContent = createPaymentLinkRes.Content.ReadAsStringAsync().Result;
                var responseData = JsonConvert.DeserializeObject<MomoOneTimePaymentCreateLinkResponse>(responseContent);
                if (responseData.resultCode == "0")
                {
                    return (true, responseData.payUrl);
                }
                else
                {
                    return (false, responseData.message);
                }
            }
            else
            {
                return (false, createPaymentLinkRes.ReasonPhrase);
            }
        }

    }
}