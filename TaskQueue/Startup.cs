using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskQueue.Controllers.Implementations;
using TaskQueue.Controllers.Interfaces;
using TaskQueue.DAL.Context;
using TaskQueue.Domain.Entities;

namespace TaskQueue
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<TaskQueueContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<TaskQueueContextInitializer>();


            services.AddScoped<IIssuesData, SqlIssuesData>();

            //Система идентификации пользователей
            //-----------------------------------------------------------------
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<TaskQueueContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(cnfg =>
            {
                cnfg.Password.RequiredLength = 3;
                cnfg.Password.RequireDigit = true;
                cnfg.Password.RequireUppercase = false;
                cnfg.Password.RequireLowercase = false;
                cnfg.Password.RequireNonAlphanumeric = false;
                cnfg.Password.RequiredUniqueChars = 3;

                cnfg.Lockout.MaxFailedAccessAttempts = 10;
                cnfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                cnfg.Lockout.AllowedForNewUsers = true;

                cnfg.User.RequireUniqueEmail = false; //TODO временно емейл может быть не уникальным
            });
            //-----------------------------------------------------------------

            //Конфигурирование Cookies
            //-----------------------------------------------------------------
            services.ConfigureApplicationCookie(cnfg => {
                cnfg.Cookie.HttpOnly = true;
                cnfg.Cookie.Expiration = TimeSpan.FromDays(150);
                cnfg.Cookie.MaxAge = TimeSpan.FromDays(150);

                cnfg.LoginPath = "/Account/Login";
                cnfg.LogoutPath = "/Account/Logout";
                cnfg.AccessDeniedPath = "/Account/AccessDenied";

                //пользователю который прошел афторизацию будет сменен номер сеанса (для повышения безопасности)
                cnfg.SlidingExpiration = true;
            });
            //-----------------------------------------------------------------

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, TaskQueueContextInitializer db)
        {
            db.InitializeAsync().Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //Соединение с браузером для обновления
                //Microsoft.VisualStudio.Web.BrowserLink
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //JS/CSS
            app.UseStaticFiles();
            //Использовать файлы по умолчанию
            app.UseDefaultFiles();
            //Cookie Policy
            app.UseCookiePolicy();
            //Подключение системы аутентификации
            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
