using CrawlData.DTO;
using CrawlData.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CrawlData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CrawlController : ControllerBase
    {
        private readonly ILogger<CrawlController> _logger;
        private readonly ICrawlService _crwalService;

        public CrawlController(ILogger<CrawlController> logger, ICrawlService crwalService)
        {
            _logger = logger;
            _crwalService = crwalService;
        }

        #region URL
        [HttpGet]
        [Route("GetAllUrls")]
        public ActionResult<BaseResponse<List<PageDTO>>> GetAllUrls() //[FromQuery(Name = "url")] string url
        {
            var result = _crwalService.GetAllUrls();
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateUrl")]
        public ActionResult<BaseResponse<PageDTO>> CreateUrl(PageDTO newUrl)
        {
            var result = _crwalService.CreateUrl(newUrl);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteUrl")]
        public ActionResult<BaseResponse<int>> DeleteUrl([FromQuery(Name = "UrlId")] int UrlId)
        {
            var result = _crwalService.DeleteUrl(UrlId);
            return Ok(result);
        }
        #endregion



        /// <returns>Trả về symbol tìm thấy</returns>
        [HttpGet]
        [Route("GetAllATags")]
        public ActionResult<BaseResponse<List<ATagDTO>>> GetAllATags([FromQuery(Name = "id")] int Url_Id) //
        {
            var result = _crwalService.GetAllATag(Url_Id);
            return Ok(result);
        }


    }
}
