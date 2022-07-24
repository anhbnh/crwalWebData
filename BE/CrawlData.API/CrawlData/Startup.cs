using CrawlAData.Services;
using CrawlData.Data;
using CrawlData.DTO.Profiles;
using CrawlData.Services.Interfaces;
using CrawlData.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrawlData
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Common._browser = new Browser();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(c => c.AddDefaultPolicy(
                                builder =>
                                {
                                    builder
                                        .SetIsOriginAllowed(origin => true)
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .AllowCredentials();
                                }
                                ));
            services.AddControllers(x => x.AllowEmptyInputInBodyModelBinding = true).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
            services.AddSession();
            services.AddAutoMapper(mc =>
            {
                mc.AddProfile<PageProfile>();
                mc.AddProfile<ATagProfile>();
            });
            services.AddDistributedMemoryCache();
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddDbContextFactory<CrawlContext>(options => options.UseSqlServer());
            // Add service
            services.AddScoped<ICrawlService, CrawlService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseCors();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
