using AutoMapper;
using CrawlData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrawlData.DTO.Profiles
{
    public class ATagProfile : Profile
    {
        public ATagProfile()
        {
            CreateMap<ATagEntity, ATagDTO>().ReverseMap();
        }
    }
}
