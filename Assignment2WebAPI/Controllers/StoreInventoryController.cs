using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Data;
using Assignment2.Models.DataModel;
using Assignment2WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment2WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/store_inventory")]
    public class StoreInventoryController : Controller
    {
        #region properties
        private readonly ApplicationDbContext _context;
        #endregion

        #region ctor
        public StoreInventoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region api/store_inventory
        /// <summary>
        /// returns store inventory for a given store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public List<REST.RESTStoreInventory> Get(int id, string product)
        {
            List<StoreInventory> items = null;
            if (string.IsNullOrEmpty(product))
            {
                items = _context.StoreInventories.Include(si => si.Product).Include(si => si.Store).Where(si => si.StoreID == id).ToList();
            }
            else {
                items = _context.StoreInventories.Include(si => si.Product).Include(si => si.Store).
                    Where(si => si.StoreID == id).Where(p => p.Product.Name.Contains(product)).ToList();
            }

            List<REST.RESTStoreInventory> response = new List<REST.RESTStoreInventory>();

            foreach (var item in items) {
                response.Add(item.ToRest());
            }

            return response;
        }
        #endregion
    }
}
