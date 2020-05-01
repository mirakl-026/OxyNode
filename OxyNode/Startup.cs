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

using OxyNode.Services;

/*
 * Проект OxyNode - совмещает GazoShop, Gazillion и NewsMaker для создания сайта продажи газоанализаторов
 */

namespace OxyNode
{
    public class Startup
    {
        // конфигурация приложения из файла appsettings.json
        public IConfiguration Config { get; }

        public Startup(IConfiguration appCfg)
        {
            // подключение файла конфигурации
            Config = appCfg;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            // сервис управления содержимым страницы "Контакты"
            services.AddTransient<ContactsService>();

            // сервис управления содержимым страницы "О нас"
            services.AddTransient<AboutService>();
            services.AddTransient<AboutSertificateService>();

            // База знаний
            // сервис управления содержимым страницы "База знаний"
            services.AddTransient<KnowledgeBaseService>();
            // сервис управления Нормативными документами
            services.AddTransient<KB_regularDocumentService>();
            // сервис управления Отраслевыми решениями
            services.AddTransient<KB_industrySolutionService>();
            // сервис управления статьями
            services.AddTransient<KB_noteService>();


            // MVC роутинг
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // создание папок ресурсов (если их нет)
            string path = env.WebRootPath + "/resources/";
            // папка для хранения сертификатов            - "/resources/about/sertificates/";
            DirectoryInfo di_about = new DirectoryInfo(path + "/about/");
            if(!di_about.Exists)
            {
                di_about.Create();
            }
            DirectoryInfo di_about_sertificates = new DirectoryInfo(path + "/about/sertificates/");
            if (!di_about_sertificates.Exists)
            {
                di_about_sertificates.Create();
            }

            // папка для хранения отраслевых решений      - "/resources/knowledgeBase/industrySolutions/";
            // папка для хранения нормативных документов  - "/resources/knowledgeBase/regularDocuments/";
            // папка для хранения статей                  - "/resources/knowledgeBase/notes/";
            DirectoryInfo di_knowledgeBase = new DirectoryInfo(path + "/knowledgeBase/");
            if (!di_knowledgeBase.Exists)
            {
                di_knowledgeBase.Create();
            }
            DirectoryInfo di_knowledgeBase_IS = new DirectoryInfo(path + "/knowledgeBase/industrySolutions/");
            if (!di_knowledgeBase_IS.Exists)
            {
                di_knowledgeBase_IS.Create();
            }
            DirectoryInfo di_knowledgeBase_RD = new DirectoryInfo(path + "/knowledgeBase/regularDocuments/");
            if (!di_knowledgeBase_RD.Exists)
            {
                di_knowledgeBase_RD.Create();
            }
            DirectoryInfo di_knowledgeBase_NT = new DirectoryInfo(path + "/knowledgeBase/notes/");
            if (!di_knowledgeBase_NT.Exists)
            {
                di_knowledgeBase_NT.Create();
            }

            // папка для хранения картинок html5 контента - "/resources/images/";
            DirectoryInfo di_images = new DirectoryInfo(path + "/images/");
            if (!di_images.Exists)
            {
                di_images.Create();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();


            app.UseRouting();

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
