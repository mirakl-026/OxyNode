using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Models;
using OxyNode.Infrastructure.Interfaces;

namespace OxyNode.Areas.admin.Controllers
{
    [Area("admin")]
    public class EditContactsController : Controller
    {
        private IContactsService _db;
        public EditContactsController(IContactsService context)
        {
            _db = context;
        }


        #region Edit "Contacts"

        // Create
        [HttpGet]
        public IActionResult CreateContacts()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateContacts(Contacts c)
        {
            if (ModelState.IsValid)
            {
                await _db.CreateContacts(c);
                return RedirectToAction("Index", "Panel");
            }
            return View(c);
        }

        // Read
        public async Task<IActionResult> ReadContacts()
        {
            var c = await _db.ReadContacts();
            return View(c);
        }

        // Update
        [HttpGet]
        public async Task<IActionResult> UpdateContacts()
        {
            var c = await _db.ReadContacts();
            return View(c);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContacts(Contacts c)
        {
            if (ModelState.IsValid)
            {
                await _db.UpdateContacts(c);
                return RedirectToAction("Index", "Panel");
            }
            return View(c);
        }


        // Delete
        public async Task<IActionResult> DeleteContacts()
        {
            await _db.DeleteContacts();
            return RedirectToAction("Index", "Panel");
        }

        #endregion
    }
}