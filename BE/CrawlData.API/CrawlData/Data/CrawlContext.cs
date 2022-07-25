using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CrawlData.Models;

namespace CrawlData.Data
{

    public class CrawlContext : DbContext
    {
        public DbSet<PageEntity> Pages { get; set; } = null;
        public DbSet<ATagEntity> ATags { get; set; } = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-TL2BUM7\\SQLEXPRESS;Initial Catalog=CrawlDB;Integrated Security=True");
        }


    }
}