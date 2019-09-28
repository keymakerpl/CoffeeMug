using CoffeeMug.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using CoffeeMug.Extensions;
using CoffeeMug.Data.Repository;
using AutoMapper;
using CoffeeMug.Data.Entities;
using CoffeeMug.Models;

namespace CoffeeMug
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["connectionStrings:CoffeeMugDbConnectionString"];
            
            #region DI Part

            services.AddDbContext<CoffeeMugDbContext>(o => o.UseSqlServer(connectionString));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAddonRepository, ProductAddonsRepository>();
            services.AddAutoMapper(cfg => 
            {
                cfg.CreateMap<ProductAddon, ProductAddonDto>();
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<ProductAddonForCreationDto, ProductAddon>();
                cfg.CreateMap<ProductForCreationDto, Product>();
                cfg.CreateMap<ProductForUpdateDto, Product>();
                cfg.CreateMap<Product, ProductForUpdateDto> ();

            }, typeof(Startup));
            
            #endregion

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Middleware MVC
            services.AddMvc()
                            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                            .AddMvcOptions(o => o.OutputFormatters.Add( new XmlDataContractSerializerOutputFormatter())) //Added output result in XML format
                            .AddJsonOptions(o => 
                            {
                                //JSON .NET - without camelCase in result
                                if (o.SerializerSettings.ContractResolver != null)
                                {
                                    var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
                                    castedResolver.NamingStrategy = null;
                                }
                            }
                            );
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CoffeeMugDbContext dbContext)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); 
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            dbContext.EnsureSeedData();

            app.UseStatusCodePages();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
