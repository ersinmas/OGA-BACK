using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Trailer, TrailerDTO>().ReverseMap();
            CreateMap<Vehicles, VehicleDTO>().ReverseMap();
            CreateMap<VehicleTrailer, VehicleTrailerDTO>().ReverseMap();
            CreateMap<VehicleType, VehicleTypeDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();

        }
    }
}
