using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Services;
using OxyNode.ViewModels;

namespace OxyNode.Controllers
{
    public class AboutController : Controller
    {
        private AboutService _db_about;
        private AboutSertificateService _db_aboutSertificate;

        public AboutController(AboutService aboutContext, AboutSertificateService aboutSertificateContext)
        {
            _db_about = aboutContext;
            _db_aboutSertificate = aboutSertificateContext;
        }


        public async Task<IActionResult> Index()
        {
            AboutViewModel avm = new AboutViewModel();

            avm.VM_About = await _db_about.ReadAbout();
            avm.VM_AboutSertificates = await _db_aboutSertificate.GetAllAboutSertificates();

            return View(avm);
        }
    }
}