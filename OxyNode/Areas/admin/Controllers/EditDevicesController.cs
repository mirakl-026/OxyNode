using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OxyNode.Infrastructure.Interfaces;
using OxyNode.Models;
using OxyNode.ViewModels;

// все действия над газоанализаторами

namespace OxyNode.Areas.admin.Controllers
{
    [Area("admin")]
    public class EditDevicesController : Controller
    {
        private IDeviceService _db;

        public EditDevicesController(IDeviceService context)
        {
            _db = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ReadAllDevices()
        {
            var dvm = new DevicesViewModel();
            dvm.devices = await _db.GetAllDevices();
            return View(dvm);
        }

        #region CRUD

        // Create
        [HttpGet]
        public IActionResult CreateDevice()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDevice(Device dev)
        {
            if (ModelState.IsValid)
            {
                await _db.CreateDevice(dev);
                return RedirectToAction("Index", "EditDevices");
            }
            return View(dev);            
        }


        // Read
        [HttpGet]
        public async Task<IActionResult> ReadDevice(string id)
        {
            var dev = await _db.ReadDevice(id);
            return View(dev);
        }


        // Update
        [HttpGet]
        public async Task<IActionResult> UpdateDevice(string id)
        {
            var dev = await _db.ReadDevice(id);
            return View(dev);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDevice(Device updDev)
        {
            if (ModelState.IsValid)
            {
                await _db.UpdateDevice(updDev);
                return RedirectToAction("Index", "EditDevices");
            }
            return View(updDev);
        }


        // Delete
        [HttpGet]
        public async Task<IActionResult> DeleteDevice(string id)
        {
            var dev = await _db.ReadDevice(id);
            return View(dev);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDevice(Device delDev)
        {
            await _db.DeleteDevice(delDev.Id);
            return RedirectToAction("Index", "EditDevices");
        }


        #endregion


    }
}
