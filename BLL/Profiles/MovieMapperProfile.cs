using AutoMapper;
using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Profiles
{
    public class MovieMapperProfile : Profile
    {
        public MovieMapperProfile()
        {
            CreateMap<MovieDTO, Movie>()
                .ForMember(m => m.Image, src => src.MapFrom(x => x.Title + ".jpg"))
                .ForMember(m => m.ReleaseDate, src => src.MapFrom(x => x.Year));
        }
    }
}
