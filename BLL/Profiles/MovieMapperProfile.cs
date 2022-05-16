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
            CreateMap<MovieGenre, MovieGenreDTO>()
                .ForMember(mg => mg.Name, src => src.MapFrom(x => x.Name))
                .ForMember(mg => mg.Id, src => src.MapFrom(x => x.Id));

            CreateMap<Movie, MovieDTO>();
        }
    }
}
