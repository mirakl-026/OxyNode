using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Services;

namespace OxyNode.Controllers
{
    // Страница "Контакты"
    public class ContactsController : Controller
    {
        private ContactsService _db;
        public ContactsController(ContactsService context)
        {
            _db = context;
        }

        public async Task<IActionResult> Index()
        {
            // считать информацию из БД
            var cc = await _db.ReadContacts();
            return View(cc);
        }
    }
}