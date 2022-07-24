using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrawlData.DTO
{
    public class ATagDTO
    {
        public long Id { get; set; }
        public int PageId { get; set; }
        public string TagId { get; set; }
        public string InnerText { get; set; }
        public string Url { get; set; }
        public string XPath { get; set; }
    }
}
