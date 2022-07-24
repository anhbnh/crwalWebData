using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CrawlData.Models
{
    public class ATagEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int PageId { get; set; }
        [MaxLength(100)]
        public string TagId { get; set; }
        public string InnerText { get; set; }
        public string Url { get; set; }
        [MaxLength(250)]
        public string XPath { get; set; }
    }
}
