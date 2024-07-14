using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.DTO.Payment;

//using Badminton.Web.DTO.Payment;
using Badminton.Web.Models;

namespace Badminton.Web.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Booking, BookingDTO>().ReverseMap();
            CreateMap<CreateBookingDTO, Booking>().ReverseMap();
            CreateMap<UpdateBookingDTO, Booking>().ReverseMap();
            CreateMap<Court, CourtDTO>().ReverseMap();
            CreateMap<CreateCourtDTO, Court>().ReverseMap();
            CreateMap<UpdateCourtDTO, Court>().ReverseMap();
            CreateMap<Evaluate, EvaluateDTO>().ReverseMap();
            CreateMap<CreateEvaluateDTO, Evaluate>().ReverseMap();
            CreateMap<UpdateEvaluateDTO, Evaluate>().ReverseMap();
            CreateMap<SubCourt, SubCourtDTO>().ReverseMap();
            CreateMap<CreateSubCourtDTO, SubCourt>().ReverseMap();
            CreateMap<UpdateSubCourtDTO, SubCourt>().ReverseMap();
            CreateMap<TimeSlot, TimeSlotDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserAdminDTO>().ReverseMap();
            CreateMap<Promotion, PromotionDTO>().ReverseMap();
            CreateMap<CreatePromotionDTO, Promotion>().ReverseMap();
            CreateMap<CheckIn, CheckInDTO>().ReverseMap();
            CreateMap<CreateCheckInDTO, CheckIn>().ReverseMap();
            CreateMap<PaymentDTO, Payment>().ReverseMap();
        }
    }
}
