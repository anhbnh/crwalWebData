using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrawlData.DTO
{
    public class PageDTO
    {
        public int PageId { get; set; }
        public string Link { get; set; }
        public bool IsGetATag { get; set; }
    }
}
