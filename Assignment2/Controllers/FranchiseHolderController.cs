using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Data;
using Assignment2.Models;
using Assignment2.Models.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Controllers
{
    [Authorize(Roles = Constants.FranchiseHolderRole)]
    [Route("[controller]/[action]")]
    public class FranchiseHolderController : Controller
    {
        #region properties
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        #endregion

        #region ctor
        public FranchiseHolderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Sidebar()
        {
            return View();
        }
        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Inventory() {
            try {
                var user = _userManager.GetUserId(User);
                var theUser = _context.Users.Where(u => u.Id == user).First();
                var model = _context.StoreInventories.Include(s => s.Product).Include(s => s.Store).Where(s => s.StoreID == theUser.StoreID).ToList();
                return View(model);
            }
            catch (Exception e) {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }
        }

        [HttpGet]
        public IActionResult StockRequest()
        {
            try
            {
                var user = _userManager.GetUserId(User);
                var theUser = _context.Users.Where(u => u.Id == user).First();
                var model = _context.StockRequests.Include(sr => sr.Product).Include(sr => sr.Store).Where(sr => sr.StoreID == theUser.StoreID).ToList();
                return View(model);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }
        }

        [HttpGet("{id}")]
        public IActionResult StockRequest(int id)
        {
            try
            {
                var user = _userManager.GetUserId(User);
                var theUser = _context.Users.Where(u => u.Id == user).First();
                var model = _context.StockRequests.Include(sr => sr.Product).Include(sr => sr.Store).Where(sr => sr.StoreID == theUser.StoreID).Where(sr => sr.ProductID == id).First();
                return View(model);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }
        }

        [HttpGet]
        public IActionResult PostStockRequest()
        {
            try
            {
                ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name");
                return View("~/Views/FranchiseHolder/Forms/StockRequest.cshtml");
            }
            catch (Exception e)
            {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult PostStockRequest(StockRequest data)
        {
            try
            {
                var user = _userManager.GetUserId(User);
                var theUser = _context.Users.Where(u => u.Id == user).First();

                // set it here, no need to muck about in the GUI
                data.StoreID = theUser.StoreID;
                //data.StoreID = 4;

                if (ModelState.IsValid)
                {
                    // if the item does not exist, add it
                    var exists = _context.StoreInventories.Where(si => si.ProductID == data.ProductID).Where(si => si.StoreID == data.StoreID).ToList();

                    if (!(exists.Count() > 0)) {
                        _context.Add(
                        new StoreInventory
                        {
                            StoreID = data.StoreID,
                            ProductID = data.ProductID,
                            StockLevel = 0
                        }
                        );
                    }

                    _context.Add(data);
                    _context.SaveChanges();
                }
                else {
                    throw new Exception("Invalid input provided");
                }

                var model = _context.StockRequests.Include(sr => sr.Product).Include(sr => sr.Store).Where(sr => sr.StoreID == theUser.StoreID).ToList();
                return View("~/Views/FranchiseHolder/StockRequest.cshtml", model);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }
        }

        //[HttpGet]
        //public IActionResult NewStockRequest()
        //{
        //    try
        //    {
        //        var inventory = _context.StoreInventories.Include(si => si.Product).ToList();
        //        var exclude = new List<Product>();

        //        foreach(var item in inventory){
        //            exclude.Add(item.Product);
        //        }

        //        // get products the store doesnt have
        //        var products = _context.Products.Except(exclude);

        //        ViewData["ProductID"] = new SelectList(products, "ProductID", "Name");
        //        return View("~/Views/FranchiseHolder/Forms/NewStockRequest.cshtml");
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.ErrorMsg = e.Message;
        //        return View("~/Views/Common/Error.cshtml");
        //    }
        //}

        //public IActionResult NewStockRequest(StockRequest data)
        //{
        //    try
        //    {
        //        var user = _userManager.GetUserId(User);
        //        var theUser = _context.Users.Where(u => u.Id == user).First();

        //        // set it here, no need to muck about in the GUI
        //        data.StoreID = theUser.StoreID;

        //        if (ModelState.IsValid)
        //        {
        //            // add the product to the inventory
        //            _context.Add(
        //                new StoreInventory {
        //                    StoreID = theUser.StoreID,
        //                    ProductID = data.ProductID,
        //                    StockLevel = 0
        //                }
        //                );

        //            _context.Add(data);
        //            _context.SaveChanges();
        //        }
        //        else
        //        {
        //            throw new Exception("Invalid input provided");
        //        }

        //        var model = _context.StockRequests.Include(sr => sr.Product).Include(sr => sr.Store).Where(sr => sr.StoreID == theUser.StoreID).ToList();
        //        return View("~/Views/FranchiseHolder/StockRequest.cshtml", model);
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.ErrorMsg = e.Message;
        //        return View("~/Views/Common/Error.cshtml");
        //    }
        //}
    }
}