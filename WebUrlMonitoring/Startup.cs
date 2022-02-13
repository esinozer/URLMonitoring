using Core.DependencyResolvers;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Identity;
using Core.Utilities.IoC;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebUrlMonitoring
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
            services.AddControllersWithViews();

            services.AddDependencyResolvers(new ICoreModule[]
           {
                new CoreModule(),
           });
            services.AddDbContext<AppDbContext>(optionsAction => {

                optionsAction.UseSqlServer(Configuration["ConnectionStrings:DefaultConnectionString"]);


            });

            services.AddIdentity<AppUser, AppRole>(opts => {
                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._ğĞçÇşŞüÜöÖıİ";
                opts.Password.RequireDigit = false;
                opts.Password.RequiredLength = 4;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireNonAlphanumeric = false;
            }).AddErrorDescriber<CustomIdentityErrorDescriber>().AddUserValidator<CustomUserValidator>().AddPasswordValidator<CustomPasswordValidator>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            CookieBuilder cookieBuilder = new CookieBuilder
            {
                Name = "monti",
                HttpOnly = false,
                SameSite = SameSiteMode.Lax,
                SecurePolicy = CookieSecurePolicy.SameAsRequest
            };
            services.ConfigureApplicationCookie(opts =>
            {
                opts.LoginPath = new PathString("/Home/LogIn");
                opts.LogoutPath = new PathString("/Member/LogOut");
                opts.Cookie = cookieBuilder;
                opts.SlidingExpiration = true;
                opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);
                // opts.AccessDeniedPath = new PathString("/Member/AccessDenied");
            });

           
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Use(async (httpContext, next) =>
            {
                log4net.GlobalContext.Properties["ipAddress"] =
                                                  httpContext?.Connection?.RemoteIpAddress.ToString();

                await next();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
