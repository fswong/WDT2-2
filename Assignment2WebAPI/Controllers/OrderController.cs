using Assignment2.Data;
using Assignment2WebAPI.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/order")]
    public class OrderController : Controller
    {
        #region properties
        private readonly ApplicationDbContext _context;
        #endregion

        #region ctor
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// returns list of orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public List<REST.Order> Get(string id)
        {
            try
            {
                var items = _context.Orders.Where(c => c.CustomerID == id).ToList();
                var response = new List<REST.Order>();
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
        public List<REST.Order> Post(REST.Order data)
        {
            try
            {
                // get the list of items in cart
                var cart = _context.Carts.Where(c => c.CustomerID == data.CustomerID).ToList();

                if (cart.Count > 0)
                {
                    foreach (var item in cart) {
                        _context.Add(
                            new Assignment2.Models.Order {
                                ProductID = item.ProductID,
                                StoreID = item.StoreID,
                                CustomerID = item.CustomerID,
                                Quantity = item.Quantity
                            }
                            );
                    }

                    // clear the cart
                    _context.Remove(cart);
                    _context.SaveChanges();
                }
                else {
                    throw new Exception("Failed to find a cart");
                }

                // return the list of orders
                var items = _context.Orders.Where(c => c.CustomerID == data.CustomerID).ToList();
                var response = new List<REST.Order>();
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
    }
}
