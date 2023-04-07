using AutoMapper;
using Doamin.DTOs;
using Doamin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.Mappers
{
    public class CarCaseMapper : Profile
    {
        public CarCaseMapper()
        {
            CreateMap<CarNumbers, CarNumberDTO>().ReverseMap();
            CreateMap<CarNumbers, GetCarNumberDTO>();
            CreateMap<OrderCase, SetOrderCaseDTO>();
            CreateMap<OrderCase, GetOrderCaseDTO>().ForMember(x => x.CarNumber, m => m.MapFrom(x => x.carNumbers));

        }

    }
}
