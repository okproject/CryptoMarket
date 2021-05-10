using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CryptoMarket.Api.Application.Abstraction;
using CryptoMarket.Api.Application.Services.ProductGateway;
using CryptoMarket.Api.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CryptoMarket.Api.Data;
using CryptoMarket.Api.Data.Repository;
using CryptoMarket.Api.Infrastructure.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CryptoMarket.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            //None
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            CurrentEnvironment = env;
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment CurrentEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (CurrentEnvironment.IsDevelopment())
            {
                services.AddDbContext<ApplicationDbContext>(
                    options => options.UseInMemoryDatabase("exampleDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite("DataSource=app.db"));
            }
            // services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IPurchaseRepository, PurchaseRepository>();

            var apiKey = Configuration.GetSection("ApiKey").Value;
            services.AddHttpClient<IProductGateway, ProductGateway>(_httpClient =>
            {
                _httpClient.BaseAddress = new Uri("https://sandbox-api.coinmarketcap.com");
                _httpClient.Timeout = new TimeSpan(0, 0, 20);
                _httpClient.DefaultRequestHeaders.Clear();

                _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", new[] {apiKey});
            }).ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip
            });

            // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Crypto Market Api v1",
                    Version = "v1"
                });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };
                // services.AddDatabaseDeveloperPageExceptionFilter();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description =
                        "JWT Authorization header using theBearer scheme.Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });

                // c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                // {
                //     
                // });
            });

            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseResponseCompression();

    

            
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crypto Market Api v1"));
            
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}