using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrawlData.Models
{
    public class PageEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PageId { get; set; }
        public string Link { get; set; }
        public bool IsGetATag { get; set; } = false;
    }
}
