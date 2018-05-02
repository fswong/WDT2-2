using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Data;
using Assignment2.Extensions.DataExtensions;
using Assignment2.Models.OwnerViewModels;
using Assignment2.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class OwnerController : Controller
    {
        #region properties
        private readonly ApplicationDbContext _context;
        #endregion

        #region ctor
        public OwnerController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// landing page for the spa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            //return View("~/Views/Dashboard/OwnerDashboard.cshtml");
            return View();
        }

        [HttpGet]
        public IActionResult Inventory() {
            var inventory = new OwnerInventoryRepository(_context).GetOwnerInventoryList();
            var response = new List<OwnerInventoryViewModel>();

            foreach (var item in inventory) {
                response.Add(item.ToViewModel(_context));
            }
            return View("~/Views/Product/OwnerInventoryList.cshtml", response);
        }

        [HttpPatch]
        [ValidateAntiForgeryToken]
        public IActionResult Inventory(string request)
        {
            var inventory = new OwnerInventoryRepository(_context).GetOwnerInventoryList();
            var response = new List<OwnerInventoryViewModel>();

            foreach (var item in inventory)
            {
                response.Add(item.ToViewModel(_context));
            }
            return View("~/Views/Product/OwnerInventoryList.cshtml", response);
        }

        [HttpGet]
        public IActionResult StockRequest() {
            return View();
        }

        [HttpDelete]
        public IActionResult StockRequest(OwnerInventoryViewModel request)
        {
            return View();
        }
    }
}