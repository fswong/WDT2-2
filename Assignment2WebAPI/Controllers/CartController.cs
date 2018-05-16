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
        public List<REST.Cart> Get(string id)
        {
            try
            {
                var items = _context.Carts.Where(c => c.CustomerID == id).ToList();
                var response = new List<REST.Cart>();
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
        public List<REST.Cart> Post(REST.Cart data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(data.ToDataModel());
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Invalid input provided");
                }

                var items = _context.Carts.Where(c => c.CustomerID == data.CustomerID).ToList();
                var response = new List<REST.Cart>();
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
        public List<REST.Cart> Put(REST.Cart data)
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
                var response = new List<REST.Cart>();
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
        [HttpDelete]
        public List<REST.Cart> Delete(REST.Cart data)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _context.Remove(data.ToDataModel());
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Invalid input provided");
                }

                var items = _context.Carts.Where(c => c.CustomerID == data.CustomerID).ToList();
                var response = new List<REST.Cart>();
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
