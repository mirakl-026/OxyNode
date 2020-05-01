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
 * ������ OxyNode - ��������� GazoShop, Gazillion � NewsMaker ��� �������� ����� ������� ����������������
 */

namespace OxyNode
{
    public class Startup
    {
        // ������������ ���������� �� ����� appsettings.json
        public IConfiguration Config { get; }

        public Startup(IConfiguration appCfg)
        {
            // ����������� ����� ������������
            Config = appCfg;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            // ������ ���������� ���������� �������� "��������"
            services.AddTransient<ContactsService>();

            // ������ ���������� ���������� �������� "� ���"
            services.AddTransient<AboutService>();
            services.AddTransient<AboutSertificateService>();

            // ���� ������
            // ������ ���������� ���������� �������� "���� ������"
            services.AddTransient<KnowledgeBaseService>();
            // ������ ���������� ������������ �����������
            services.AddTransient<KB_regularDocumentService>();
            // ������ ���������� ����������� ���������
            services.AddTransient<KB_industrySolutionService>();


            // MVC �������
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // �������� ����� �������� (���� �� ���)


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
