using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Infrastructure.Interfaces;
using OxyNode.ViewModels;

namespace OxyNode.Controllers
{
    public class AboutController : Controller
    {
        private IAboutService _db_about;
        private IAboutSertificateService _db_aboutSertificate;

        public AboutController(IAboutService aboutContext, IAboutSertificateService aboutSertificateContext)
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