using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Controllers
{
    [Authorize(Roles = Constants.CustomerRole)]
    [Route("[controller]/[action]")]
    public class CustomerController : Controller
    {
        #region properties
        private readonly ApplicationDbContext _context;
        #endregion

        #region ctor
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult Inventory(int id) {
            var model = _context.StoreInventories.Include(si => si.Product).Include(si => si.Store).Where(si => si.StoreID == id).
                ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Cart()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cart(int data)
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public IActionResult Sidebar()
        {
            // stores list
            var stores = _context.Stores.ToList();
            return View(stores);
        }
    }
}