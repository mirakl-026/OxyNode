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

//using OxyNode.Services;

using OxyNode.Infrastructure.Interfaces;
using OxyNode.Services.MongoDB;

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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            string path = env.WebRootPath + "/resources/";
            // "/resources/about/sertificates/";
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

            // "/resources/knowledgeBase/industrySolutions/";
            // "/resources/knowledgeBase/regularDocuments/";
            // "/resources/knowledgeBase/notes/";
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

            // "/resources/images/";
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
