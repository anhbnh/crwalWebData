using AutoMapper;
using CrawlData.Models;

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
