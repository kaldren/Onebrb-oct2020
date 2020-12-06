using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Onebrb.Core.Models;
using Onebrb.Data;
using Onebrb.Services.Items;
using Onebrb.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Onebrb.Api
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // TODO: Make db path futureproof
            //services.AddDbContext<OnebrbContext>(options => options.UseSqlite(@"Data Source=C:\Users\drens\source\repos\October2020\Onebrb\src\Onebrb.Data\Onebrb.db"));

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder
                        .WithOrigins("https://localhost:44362", "https://localhost:44307")
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    }
                );
            });

            services.AddDbContext<OnebrbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));

            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                            .AddEntityFrameworkStores<OnebrbContext>();

            services.AddAutoMapper(typeof(Startup));

            // Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = Configuration["AAD:ResourceId"];
                    options.Authority = $"{Configuration["AAD:InstanceId"]}{Configuration["AAD:TenantId"]}";
                });


            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IOnebrbContext, OnebrbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
