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
            try {
                var inventory = HttpClientHelper.GetCollection<Assignment2WebAPI.REST.RESTStoreInventory>($"store_inventory/{id}");
                return View(inventory.data);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }
        }

        /// <summary>
        /// displays the current cart
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Cart()
        {
            try
            {
                var user = _userManager.GetUserId(User);
                var cart = HttpClientHelper.GetCollection<Assignment2WebAPI.REST.RESTCart>($"cart/{user}");
                ViewBag.CustomerID = user;

                return View(cart.data);

            }
            catch (Exception e)
            {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }
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
            try
            {
                HttpClientHelper.Post<Assignment2WebAPI.REST.RESTCart>(data.ToHttpBody(), "cart");

                var user = _userManager.GetUserId(User);
                var cart = HttpClientHelper.GetCollection<Assignment2WebAPI.REST.RESTCart>($"cart/{user}");
                return View(cart.data);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }
        }

        /// <summary>
        /// display the form
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CartForm(int? StoreID, int? ProductID)
        {
            try {
                if (StoreID == null || ProductID == null) {
                    throw new Exception("Invalid information provided");
                }

                var user = _userManager.GetUserId(User);

                ViewBag.StoreID = StoreID;
                ViewBag.ProductID = ProductID;
                ViewBag.CustomerID = user;
                return View("~/Views/Customer/Forms/CartForm.cshtml");
            }
            catch (Exception e)
            {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }
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
            try
            {
                // stores list
                var stores = _context.Stores.ToList();
                return View(stores);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }
            
        }

        /// <summary>
        /// ask for the payment details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Checkout()
        {
            try
            {
                var user = _userManager.GetUserId(User);
                var cart = HttpClientHelper.GetCollection<Assignment2WebAPI.REST.RESTCart>($"cart/{user}");

                if (cart.data.Count() == 0)
                {
                    throw new Exception("No products in cart");
                }

                return View(new CreditCardViewModel { CreditCardType = CardType.MasterCard });
            }
            catch (Exception e)
            {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }
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
            var orderId = Guid.NewGuid();

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

                var user = _userManager.GetUserId(User);
                var cart = HttpClientHelper.GetCollection<Assignment2WebAPI.REST.RESTCart>($"cart/{user}");

                if (cart.data.Count() == 0) {
                    throw new Exception("No items in cart");
                }

                

                //create the order
                _context.Add(new OrderHeader { OrderMain = orderId.ToString() });

                foreach (var item in cart.data)
                {
                    // add the order
                    _context.Add(
                        new Assignment2.Models.Order
                        {
                            ProductID = item.ProductID,
                            OrderMain = orderId.ToString(),
                            StoreID = item.StoreID,
                            CustomerID = item.CustomerID,
                            Quantity = item.Quantity
                        }
                        );

                    // clear the cart
                    var cartitem = _context.Carts.Where(c => c.ProductID == item.ProductID)
                        .Where(c => c.StoreID == item.StoreID).First();
                    _context.Remove(cartitem);

                    // reduce the stock
                    var storeInventory = _context.StoreInventories.Where(c => c.StoreID == item.StoreID)
                        .Where(c => c.ProductID == item.ProductID).First();
                    storeInventory.StockLevel = storeInventory.StockLevel - item.Quantity;
                    _context.Remove(storeInventory);
                }

                
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }

            return RedirectToAction("Receipt", "Customer", orderId);
        }

        /// <summary>
        /// returns a list of past orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Order()
        {
            try {
                var user = _userManager.GetUserId(User);
                var order = HttpClientHelper.GetCollection<Assignment2WebAPI.REST.RESTOrder>($"order/{user}");
                return View(order.data);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }
        }

        /// <summary>
        /// on success display receipt page
        /// </summary>
        /// <returns></returns>
        public IActionResult Receipt(string OrderMain) {
            try {
                ViewBag.OrderMain = OrderMain;
                var orders = _context.Orders.Include(o => o.Product).Include(o => o.Store).Where(o => o.OrderMain == OrderMain).ToList();

                return View(orders);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }
        }
    }
}