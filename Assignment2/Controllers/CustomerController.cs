using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Data;
using Assignment2.Helpers;
using Assignment2.Models;
using Assignment2WebAPI.Extensions;
using Assignment2WebAPI.REST;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private UserManager<ApplicationUser> _userManager;
        #endregion

        #region ctor
        public CustomerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult StoreInventory(int id) {
            var inventory = HttpClientHelper.GetCollection<Assignment2WebAPI.REST.RESTStoreInventory>($"store_inventory/{id}");
            return View(inventory.data);
        }

        [HttpGet]
        public IActionResult Cart()
        {
            var user = _userManager.GetUserId(User);
            var cart = HttpClientHelper.GetCollection<Assignment2WebAPI.REST.RESTCart>($"cart/{user}");
            return View(cart.data);
        }

        [HttpPost]
        public IActionResult Cart(RESTCart data)
        {
            HttpClientHelper.Post<Assignment2WebAPI.REST.RESTCart>(data.ToHttpBody(), "cart");

            var user = _userManager.GetUserId(User);
            var cart = HttpClientHelper.GetCollection<Assignment2WebAPI.REST.RESTCart>($"cart/{user}");
            return View(cart.data);
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

        [HttpGet]
        public IActionResult Checkout()
        {
            var user = _userManager.GetUserId(User);
            var cart = HttpClientHelper.GetCollection<Assignment2WebAPI.REST.RESTCart>($"cart/{user}");
            return View(cart.data);
        }

        [HttpPost]
        public IActionResult Checkout(string creditcard)
        {
            var user = _userManager.GetUserId(User);
            var cart = HttpClientHelper.GetCollection<Assignment2WebAPI.REST.RESTCart>($"cart/{user}");
            return View(cart.data);
        }

        [HttpGet]
        public IActionResult Order()
        {
            var user = _userManager.GetUserId(User);
            var order = HttpClientHelper.GetCollection<Assignment2WebAPI.REST.RESTOrder>($"order/{user}");
            return View(order.data);
        }
    }
}