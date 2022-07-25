using AutoMapper;
using CrawlData.Models;

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
