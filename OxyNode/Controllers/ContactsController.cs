using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OxyNode.Controllers
{
    // Страница "Контакты"
    public class ContactsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            // await на связь с БДыыыы
            return View();
        }
    }
}