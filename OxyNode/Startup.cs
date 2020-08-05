using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

//using OxyNode.Services;

using OxyNode.Infrastructure.Interfaces;
using OxyNode.Infrastructure.Interfaces.FileSystem;
using OxyNode.Services.MongoDB;
using OxyNode.Services.FileSystem;
using OxyNode.Services.Identity;
using OxyNode.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace OxyNode
{
    public class Startup
    {
        
        public IConfiguration Config { get; }

        public Startup(IConfiguration appCfg)
        {
            Config = appCfg;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            // SQL Server Express
            services.AddDbContext<OxyNodeEntitiesContext>(opt =>
                opt.UseSqlServer(Config.GetConnectionString("DefaultConnection")));

            services.AddTransient<OxyNodeEntitiesContextInitializer>();

            // Identity
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<OxyNodeEntitiesContext>()
                .AddDefaultTokenProviders();

            
            services.Configure<IdentityOptions>(opt => 
            {
                // параметры системы identity
                opt.Password.RequiredLength = 6;    // длина пароля
                opt.Password.RequireDigit = true;   // обязательно должны быть цифры
                opt.Password.RequireUppercase = false;  // отключить требование символов верхнего регистра
                opt.Password.RequireLowercase = false;  // отключить требование символов нижнего регистра
                opt.Password.RequiredUniqueChars = 4;   // кол-во уникальных символов

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 7;    // кол-во ошибок входа до блокировки
                opt.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(30);   // время блокировки

                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWYZ_1234567890"; // доступные символы для логина
                opt.User.RequireUniqueEmail = true;    // обязательно уникальный e-mail
            });

            // конфигурация cookie
            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "OxyNode";     // имя Cookie в браузере
                opt.Cookie.HttpOnly = true; // передача толькопо http
                opt.ExpireTimeSpan = System.TimeSpan.FromDays(30); // время жизни Cookie - 30 дней

                opt.LoginPath = "/Account/Login";   // автоматические перенаправление, при отсутствии логина и запрету ресурсов
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied"; // путь при отказе в доступе

                opt.SlidingExpiration = true;   // автоматически изменять идентификатор сеанса если пользователь меняет состояние

            });
            


            // File System
            services.AddTransient<IFileAboutSertificateService, FS_AboutSertificateService>();
            services.AddTransient<IFileImageService, FS_ImageService>();
            services.AddTransient<IFileRegularDocumentService, FS_RegularDocumentService>();
            services.AddTransient<IFileIndustrySolutionService, FS_IndustrySolutionService>();

            // Mongo DB
            services.AddTransient<IContactsService, MDB_ContactsService>();

            services.AddTransient<IAboutService, MDB_AboutService>();
            services.AddTransient<IAboutSertificateService, MDB_AboutSertificateService>();
          
            services.AddTransient<IKnowledgeBaseService, MDB_KnowledgeBaseService>();
            
            services.AddTransient<IKB_regularDocumentService, MDB_KB_regularDocumentService>();
            
            services.AddTransient<IKB_industrySolutionService, MDB_KB_industrySolutionService>();
            
            services.AddTransient<IKB_noteService, MDB_KB_noteService>();

            //services.AddTransient<IKB_questionService, MDB_KB_questionService>();
            //services.AddTransient<IKB_answerService, MDB_KB_answerService>();
            services.AddTransient<IKB_QAService, MDB_KB_QAService>();

            services.AddTransient<INewsService, MDB_NewsService>();

            services.AddTransient<IDeviceService, MDB_DeviceService>();
            
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, OxyNodeEntitiesContextInitializer db)
        {
            db.InitializeAsync().Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            string path = env.WebRootPath + "/resources/";

            // AboutSertificatesPath = "/resources/aboutSertificates/";
            // ImagesPath = "/resources/images/";
            // IndustrySolutionsPath = "/resources/industrySolutions/";
            // RegularDocumentsPath = "/resources/regularDocuments/";
            DirectoryInfo di_aboutSertificates = new DirectoryInfo(path + "/aboutSertificates/");
            if(!di_aboutSertificates.Exists)
            {
                di_aboutSertificates.Create();
            }
            DirectoryInfo di_images = new DirectoryInfo(path + "/images/");
            if (!di_images.Exists)
            {
                di_images.Create();
            }
            DirectoryInfo di_industrySolutions = new DirectoryInfo(path + "/industrySolutions/");
            if (!di_industrySolutions.Exists)
            {
                di_industrySolutions.Create();
            }
            DirectoryInfo di_regularDocuments = new DirectoryInfo(path + "/regularDocuments/");
            if (!di_regularDocuments.Exists)
            {
                di_regularDocuments.Create();
            }



            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "admin",
                    pattern: "{area:exists}/{controller=Panel}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
