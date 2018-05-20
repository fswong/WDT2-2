using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Assignment2.Data;
using Assignment2.Models;
using Assignment2.Models.DataModel;
using Assignment2WebAPI.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Assignment2WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/cart")]
    public class CartController : Controller
    {
        #region properties
        private readonly ApplicationDbContext _context;
        #endregion

        #region ctor
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// returns list of items in cart
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public List<REST.RESTCart> Get(string id)
        {
            try
            {
                var items = _context.Carts.Include(c => c.Product).Include(c => c.Store).Where(c => c.CustomerID == id).ToList();
                var response = new List<REST.RESTCart>();
                foreach (var item in items)
                {
                    response.Add(item.ToRest());
                }

                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// add new item to cart
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public List<REST.RESTCart> Post(REST.RESTCart data)
        {
            try
            {
                if (data.Quantity < 1) {
                    throw new Exception("Invalid quantity provided");
                }

                if (ModelState.IsValid)
                {
                    // check if item exists
                    var exists = _context.Carts.Where(c => c.ProductID == data.ProductID)
                        .Where(c => c.StoreID == data.StoreID)
                        .Where(c => c.CustomerID == data.CustomerID).FirstOrDefault();

                    var quantity = 0;

                    if (exists == null)
                    {
                        quantity = data.Quantity;
                        _context.Add(data.ToDataModel());
                    }
                    else {
                        exists.Quantity += data.Quantity;

                        quantity = exists.Quantity;
                        _context.Update(exists);
                    }

                    // check the stock level
                    var storeInventory = _context.StoreInventories.Where(c => c.StoreID == data.StoreID).Where(c => c.ProductID == data.ProductID).FirstOrDefault();
                    if (storeInventory == null)
                    {
                        throw new Exception("Invalid Item chosen");
                    }
                    else {
                        if (quantity > storeInventory.StockLevel) {
                            throw new Exception("Insufficient stock");
                        }
                        _context.SaveChanges();
                    }
                    
                }
                else
                {
                    throw new Exception("Invalid input provided");
                }
                var items = _context.Carts.Include(c => c.Product).Include(c => c.Store).Where(c => c.CustomerID == data.CustomerID).ToList();
                var response = new List<REST.RESTCart>();
                foreach (var item in items)
                {
                    response.Add(item.ToRest());
                }

                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// add new item to cart
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public List<REST.RESTCart> Put(REST.RESTCart data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(data.ToDataModel());
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Invalid input provided");
                }

                var items = _context.Carts.Where(c => c.CustomerID == data.CustomerID).ToList();
                var response = new List<REST.RESTCart>();
                foreach (var item in items) {
                    response.Add(item.ToRest());
                }

                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// removes item from cart
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpDelete("{CustomerID}/{StoreID}/{ProductID}")]
        public void Delete([FromRoute]string CustomerID, int StoreID, int ProductID)
        {
            try
            {
                //if (StoreID == null || ProductID == null) {
                //    throw new Exception("Invalid input provided");
                //}

                var exists = _context.Carts.Where(c => c.ProductID == ProductID)
                        .Where(c => c.StoreID == StoreID)
                        .Where(c => c.CustomerID == CustomerID).FirstOrDefault();

                if (exists != null)
                {
                    _context.Remove(exists);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Invalid input provided");
                }
               
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
