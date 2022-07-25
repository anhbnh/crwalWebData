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
        private readonly ICrawlService _crawlService;

        public CrawlController(ILogger<CrawlController> logger, ICrawlService crawlService)
        {
            _logger = logger;
            _crawlService = crawlService;
        }

        #region URL
        [HttpGet]
        [Route("GetAllUrls")]
        public ActionResult<BaseResponse<List<PageDTO>>> GetAllUrls()
        {
            var result = _crawlService.GetAllUrls();
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateUrl")]
        public ActionResult<BaseResponse<PageDTO>> CreateUrl(PageDTO newUrl)
        {
            var result = _crawlService.CreateUrl(newUrl);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteUrl")]
        public ActionResult<BaseResponse<int>> DeleteUrl([FromQuery(Name = "UrlId")] int UrlId)
        {
            var result = _crawlService.DeleteUrl(UrlId);
            return Ok(result);
        }
        #endregion

        [HttpGet]
        [Route("GetAllATags")]
        public ActionResult<BaseResponse<List<ATagDTO>>> GetAllATags([FromQuery(Name = "id")] int Url_Id)
        {
            var result = _crawlService.GetAllATag(Url_Id);
            return Ok(result);
        }


    }
}
