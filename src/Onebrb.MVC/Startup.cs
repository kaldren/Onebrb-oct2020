using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Onebrb.Data;
using Onebrb.Core.Models;
using AutoMapper;
using Onebrb.MVC.Constants;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Net.Http;
using Onebrb.MVC.Services;
using OnebrbApiClient;

namespace Onebrb.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OnebrbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(AppConst.ConnectionString)));

            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<OnebrbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            services.AddHttpContextAccessor();

            services.AddControllersWithViews();
            services.AddRazorPages();

            // Localization
            services.AddLocalization(options => options.ResourcesPath = AppConst.ResourcesPath);
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("bg")
                    };

                    options.DefaultRequestCulture = new RequestCulture(culture: "bg", uiCulture: "bg");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;

                    options.RequestCultureProviders.Clear();
                    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
                    //options.RequestCultureProviders.Insert(1, new RouteDataRequestCultureProvider());
                    //options.RequestCultureProviders.Insert(2, new QueryStringRequestCultureProvider());
                }
            );

            services
                .AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.AddServerSideBlazor();

            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IOnebrbContext, OnebrbContext>();
            services.AddTransient<IApiService, ApiService>();

            // Autorest
            //services.AddTransient<IOnebrbApi, OnebrbApi>();

            services.AddHttpClient();
            services.AddScoped<HttpClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var localizeOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizeOptions.Value);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
            });
        }
    }
}
