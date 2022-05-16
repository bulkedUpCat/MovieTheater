﻿using AutoMapper;
using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Profiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserLoginDTO>().ReverseMap();
            CreateMap<User, UserRegisterDTO>().ReverseMap();

            CreateMap<User, UserDTO>();
        }
    }
}
