﻿using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Models;
using System.Globalization;

namespace Badminton.Web.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Booking, BookingDTO>();
            CreateMap<UpdateBookingDTO, BookingDTO>();
            CreateMap<Schedule, ScheduleDTO>();
            CreateMap<ScheduleDTO, Schedule>();
            CreateMap<CreateScheduleDTO, Schedule>();
            CreateMap<Court, CourtDTO>();
            CreateMap<CreateCourtDTO, Court>();
            CreateMap<Evaluate, EvaluateDTO>();
            CreateMap<CreateEvaluateDTO, Evaluate>();
            CreateMap<UpdateEvaluateDTO, Evaluate>();
            CreateMap<SubCourt, SubCourtDTO>();
            CreateMap<CreateSubCourtDTO, SubCourt>();
            CreateMap<TimeSlot, TimeSlotDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<User, UserAdminDTO>();
            CreateMap<Promotion, PromotionDTO>();
            CreateMap<CreatePromotionDTO, Promotion>();
        }
    }
}
