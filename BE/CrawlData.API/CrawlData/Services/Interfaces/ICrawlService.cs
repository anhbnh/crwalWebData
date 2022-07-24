using CrawlData.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrawlData.Services.Interfaces
{
    public interface ICrawlService
    {
        public BaseResponse<List<PageDTO>> GetAllUrls();
        public BaseResponse<PageDTO> CreateUrl(PageDTO newUrl);
        public BaseResponse<int> DeleteUrl(int UrlId);

        public BaseResponse<List<ATagDTO>> GetAllATag(int Url_Id);
    }
}
