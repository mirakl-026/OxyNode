using System;
using System.Collections.Generic;
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

            // MVC роутинг
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
