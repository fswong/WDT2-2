using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Common.Enums;
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
            return RedirectToAction("Cart", "Customer");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult StoreInventory(int id) {
            var inventory = HttpClientHelper.GetCollection<Assignment2WebAPI.REST.RESTStoreInventory>($"store_inventory/{id}");
            return View(inventory.data);
        }

        /// <summary>
        /// displays the current cart
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Cart()
        {
            var user = _userManager.GetUserId(User);
            var cart = HttpClientHelper.GetCollection<Assignment2WebAPI.REST.RESTCart>($"cart/{user}");
            return View(cart.data);
        }

        /// <summary>
        /// add an item to cart
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cart(RESTCart data)
        {
            HttpClientHelper.Post<Assignment2WebAPI.REST.RESTCart>(data.ToHttpBody(), "cart");

            var user = _userManager.GetUserId(User);
            var cart = HttpClientHelper.GetCollection<Assignment2WebAPI.REST.RESTCart>($"cart/{user}");
            return View(cart.data);
        }

        /// <summary>
        /// forgot what I added this for
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// generic sidebar
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Sidebar()
        {
            // stores list
            var stores = _context.Stores.ToList();
            return View(stores);
        }

        /// <summary>
        /// ask for the payment details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new CreditCardViewModel { CreditCardType = CardType.MasterCard });
        }

        /// <summary>
        /// places the actual order
        /// </summary>
        /// <param name="creditcard"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(CreditCardViewModel creditcard)
        {
            try
            {
                // Validate card type.
                CardType expectedCardType = CreditCardHelper.GetCardType(creditcard.CreditCardNumber);
                if (expectedCardType == CardType.Unknown || expectedCardType != creditcard.CreditCardType)
                {
                    ModelState.AddModelError("CreditCardType", "The Credit Card Type field does not match against the credit card number.");
                }

                if (!ModelState.IsValid)
                {
                    throw new Exception("Creditcard is invalid");
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }

            return RedirectToAction("Receipt", "Customer");
        }

        [HttpGet]
        public IActionResult Order()
        {
            var user = _userManager.GetUserId(User);
            var order = HttpClientHelper.GetCollection<Assignment2WebAPI.REST.RESTOrder>($"order/{user}");
            return View(order.data);
        }

        public IActionResult Receipt() {
            return View();
        }
    }
}