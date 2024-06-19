using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Models;

namespace Badminton.Web.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateBookingDTO, Booking>();
            CreateMap<Booking, BookingDTO>();

            /*CreateMap<Booking, BookingDTO>()
                .ForMember(dest => dest.TimeSlot, opt => opt.MapFrom(src => src.TimeSlot))
                .ForMember(dest => dest.SubCourt, opt => opt.MapFrom(src => src.SubCourt)); // Ánh xạ các DTO liên quan
            
            CreateMap<CreateBookingDTO, Booking>();

            CreateMap<UpdateBookingDTO, Booking>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));*/

            // Map từ Schedule sang ScheduleDTO
            CreateMap<Schedule, ScheduleDTO>()
               /* .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))*/
                .ForMember(dest => dest.SubCourt, opt => opt.MapFrom(src => src.SubCourt));

            // Map từ ScheduleDTO sang Schedule (nếu cần thiết)
            CreateMap<ScheduleDTO, Schedule>()
                /*.ForMember(dest => dest.User, opt => opt.Ignore())*/
                .ForMember(dest => dest.SubCourt, opt => opt.Ignore());


            CreateMap<Court, CourtDTO>();
            CreateMap<CreateCourtDTO, Court>();
            CreateMap<Evaluate, EvaluateDTO>();
            CreateMap<CreateEvaluateDTO, Evaluate>();
            CreateMap<UpdateEvaluateDTO, Evaluate>();
            CreateMap<SubCourt, SubCourtDTO>();
            CreateMap<CreateSubCourtDTO, SubCourt>();
            CreateMap<TimeSlot, TimeSlotDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}
