﻿using ArchitecturalApplication.Core.Dtos;
using ArchitecturalApplication.Core.Models;
using AutoMapper;

namespace ArchitecturalApplication.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<Gig, GigDto>();
                cfg.CreateMap<Notification, NotificationDto>();
            });
            config.CreateMapper();
        }
    }
}