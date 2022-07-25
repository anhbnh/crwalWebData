using System;
using System.Collections.Generic;
using System.Linq;
using CefSharp;
using System.Net;
using HtmlAgilityPack;
using CrawlData.Services.Interfaces;
using CrawlData.DTO;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CrawlData.Data;
using CrawlData.Models;
using CrawlData.Utils;

namespace CrawlAData.Services
{
    public class CrawlService : ICrawlService
    {
        private readonly IDbContextFactory<CrawlContext> _contextFactory;
        private readonly IMapper _mapper;

        public CrawlService(IDbContextFactory<CrawlContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        #region Url
        public BaseResponse<List<PageDTO>> GetAllUrls()
        {
            var context = _contextFactory.CreateDbContext();
            return new BaseResponse<List<PageDTO>>(true, context.Pages.Select(x => _mapper.Map<PageDTO>(x)).ToList());
        }

        public BaseResponse<PageDTO> CreateUrl(PageDTO newUrl)
        {
            try
            {
                var context = _contextFactory.CreateDbContext();
                if (context.Pages.Any(x => x.Link == newUrl.Link)) return new BaseResponse<PageDTO>(false, newUrl, "This Url is existed.");
                newUrl.PageId = 0;
                newUrl.IsGetATag = false;
                var newUrlEntity = _mapper.Map<PageEntity>(newUrl);
                var savedUrl = context.Pages.Add(newUrlEntity);
                var saved = context.SaveChanges();
                if (saved < 1) return new BaseResponse<PageDTO>(false, newUrl, "Failed");
                newUrl.PageId = savedUrl.Entity.PageId;
                return new BaseResponse<PageDTO>(true, newUrl);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PageDTO>(false, newUrl, ex.Message);
            }
        }

        public BaseResponse<int> DeleteUrl(int UrlId)
        {
            try
            {
                var context = _contextFactory.CreateDbContext();
                var Url = context.Pages.FirstOrDefault(x => x.PageId == UrlId);
                if (Url == null)
                    return new BaseResponse<int>(false, -1, "This Url is not existed.");
                else
                {
                    if (Url.IsGetATag)
                    {
                        var aTags = context.ATags.Where(x => x.PageId == UrlId).ToList();
                        if (aTags.Count > 0)
                        {
                            context.ATags.RemoveRange(aTags);
                        }
                    }
                }
                context.Pages.Remove(Url);
                var saved = context.SaveChanges();
                if (saved < 1) return new BaseResponse<int>(false, -1, "Failed");
                return new BaseResponse<int>(true, 1);
            }
            catch (Exception ex)
            {
                return new BaseResponse<int>(false, -1, ex.Message);
            }
        }
        #endregion

        #region ATag
        private List<string> lstCSRPage = new List<string>() { "https://www.facebook.com/" };

        public BaseResponse<List<ATagDTO>> GetAllATag(int PageId)
        {
            var context = _contextFactory.CreateDbContext();
            var _Url = context.Pages.FirstOrDefault(x => x.PageId == PageId);
            if (_Url != null)
            {
                if (_Url.IsGetATag)
                {
                    return new BaseResponse<List<ATagDTO>>(true, context.ATags.Where(x => x.PageId == _Url.PageId).Select(x => _mapper.Map<ATagDTO>(x)).ToList());
                }
            }
            else
            {
                return new BaseResponse<List<ATagDTO>>(true, null, "Id is not existed.");
            }

            bool isSSR = true;
            foreach (var url in lstCSRPage)
            {
                if (_Url.Link.StartsWith(url))
                {
                    isSSR = false;
                    break;
                }
            }
            List<ATagDTO> lst = null;
            if (isSSR)
            {
                WebClient client = new WebClient();
                string InnerHtml = client.DownloadString(_Url.Link);

                // Nếu là SSR page
                if (CheckCSRPage(InnerHtml))
                {
                    InnerHtml = DowLoadCSR(_Url.Link);
                }
                lst = CrawlATag(InnerHtml);
            }
            else
            {
                string InnerHtml = DowLoadCSR(_Url.Link);
                lst = CrawlATag(InnerHtml);
            }

            // lưu vào DB
            IList<ATagEntity> newtags = new List<ATagEntity>();
            foreach (var item in lst)
            {
                item.PageId = _Url.PageId;
                var tag = _mapper.Map<ATagEntity>(item);
                newtags.Add(tag);
            }
            try
            {
                context.ATags.AddRange(newtags);
                _Url.IsGetATag = true;
                context.SaveChanges();
            }
            catch
            {

            }
            return new BaseResponse<List<ATagDTO>>(true, lst);
        }

        private string DowLoadCSR(string sUrl)
        {
            Common._browser.OpenUrl(sUrl);
            string s = Common._browser.Page.GetSourceAsync().Result;
            return s;
        }


        public bool CheckCSRPage(string InnerHtml)
        {
            var Html = new HtmlDocument();
            Html.LoadHtml(InnerHtml);
            var root = Html.DocumentNode;
            var noscripts = root.Descendants("noscript");
            foreach (var item in noscripts)
            {
                if (item.ParentNode.Name == "body")
                {
                    if (item.InnerText.Contains("Không thể tải ứng dụng, vui lòng kích hoạt JavaScript rồi tải lại trang.")
                        || item.InnerText.Contains("You need to enable JavaScript to run this app."))
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public List<ATagDTO> CrawlATag(string InnerHtml)
        {
            var Html = new HtmlDocument();
            Html.LoadHtml(InnerHtml);
            var root = Html.DocumentNode;
            var anchors = root.Descendants("a");
            List<ATagDTO> lst = new List<ATagDTO>();
            foreach (var item in anchors)
            {
                ATagDTO oo = new ATagDTO();
                oo.TagId = item.Id;
                oo.InnerText = item.InnerText.Trim();
                foreach (var attribute in item.Attributes)
                {
                    if (attribute.Name == "href")
                    {
                        oo.Url = attribute.Value;
                        break;
                    }
                }
                oo.XPath = item.XPath;
                if (string.IsNullOrWhiteSpace(oo.Url) && string.IsNullOrWhiteSpace(oo.InnerText))
                {
                    continue;
                }
                lst.Add(oo);
            }
            return lst;
        }
        #endregion
    }
}
