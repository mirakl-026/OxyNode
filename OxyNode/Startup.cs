﻿using System;
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
using OxyNode.Infrastructure.Interfaces.FileSystem;
using OxyNode.Services.MongoDB;
using OxyNode.Services.FileSystem;

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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
