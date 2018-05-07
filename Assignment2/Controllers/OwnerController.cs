﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Data;
using Assignment2.Extensions.DataExtensions;
using Assignment2.Models.DataModel;
using Assignment2.Models.OwnerViewModels;
using Assignment2.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return View();
        }

        [HttpGet]
        public IActionResult Sidebar()
        {
            return View();
        }

        /// <summary>
        /// used to view owner inventory
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Inventory() {
            var inventory = new OwnerInventoryRepository(_context).GetOwnerInventoryList();
            var response = new List<OwnerInventoryViewModel>();

            foreach (var item in inventory) {
                response.Add(item.ToViewModel(_context));
            }
            return View("~/Views/Product/OwnerInventoryList.cshtml", response);
        }

        /// <summary>
        /// used to set stock owner inventory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch]
        [ValidateAntiForgeryToken]
        public IActionResult Inventory(int id, OwnerInventory data)
        {
            try {
                //validate post
                if (id != data.ProductID) {
                    throw new Exception("Invalid product id provided");
                }

                _context.Update(data);
                _context.SaveChanges();

                var response = _context.OwnerInventories.Include(oi => oi.Product).ToList();

                return View("~/Views/Product/OwnerInventoryList.cshtml", response);
            }
            catch (Exception e) {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }
        }

        /// <summary>
        /// used to get stock request
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult StockRequest() {
            var model = _context.StockRequests.Include(s => s.Product).Include(s => s.Store).ToList();
            return View(model);
        }

        /// <summary>
        /// used to fulfil stock request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult StockRequest(int id)
        {
            try {
                var stockRequest = _context.StockRequests.Where(sr => sr.StockRequestID == id).First();
                var ownerInventory = _context.OwnerInventories.Where(oi => oi.ProductID == stockRequest.ProductID).First();

                if (ownerInventory.StockLevel >= stockRequest.Quantity)
                {
                    var changeAmount = stockRequest.Quantity;

                    // reduce stock level
                    ownerInventory.StockLevel -= changeAmount;
                    _context.Update(ownerInventory);

                    // add the stock
                    var storeInventory = _context.StoreInventories
                        .Where(si => si.StoreID == stockRequest.StoreID)
                        .Where(si => si.ProductID == stockRequest.ProductID).First();

                    storeInventory.StockLevel += changeAmount;
                    _context.Update(storeInventory);

                    // delete the request
                    _context.Remove(stockRequest);
                    _context.SaveChanges();

                    var model = _context.StockRequests.Include(s => s.Product).Include(s => s.Store).ToList();
                    return View(model);
                }
                else {
                    throw new Exception("Insufficient stock to process your order");
                }
            } catch (Exception e) {
                ViewBag.ErrorMsg = e.Message;
                return View("~/Views/Common/Error.cshtml");
            }
        }
    }
}