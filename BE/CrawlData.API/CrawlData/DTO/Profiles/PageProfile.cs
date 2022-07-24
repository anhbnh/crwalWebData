using AutoMapper;
using CrawlData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrawlData.DTO.Profiles
{
    public class PageProfile : Profile
    {
        public PageProfile()
        {
            CreateMap<PageEntity, PageDTO>().ReverseMap();
        }
    }
}
