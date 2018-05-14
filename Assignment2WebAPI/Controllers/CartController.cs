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
        /// returns list of stores
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public List<Cart> Get(string id)
        {
            try
            {
                return _context.Carts.Where(c => c.CustomerID == id).ToList();
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
        public List<Cart> Post(Cart data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(data);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Invalid input provided");
                }

                return _context.Carts.Where(c => c.CustomerID == data.CustomerID).ToList();
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
        public List<Cart> Put(Cart data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(data);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Invalid input provided");
                }

                return _context.Carts.Where(c => c.CustomerID == data.CustomerID).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpDelete]
        public List<Cart> Delete(Cart data)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _context.Remove(data);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Invalid input provided");
                }

                return _context.Carts.Where(c => c.CustomerID == data.CustomerID).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
