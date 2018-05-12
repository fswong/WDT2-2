using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Assignment2.Data;
using Assignment2.Models.DataModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Assignment2WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/stores")]
    public class StoresController : Controller
    {
        #region properties
        private readonly ApplicationDbContext _context;
        #endregion

        #region ctor
        public StoresController(ApplicationDbContext context) {
            _context = context;
        }
        #endregion

        /// <summary>
        /// returns list of stores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Store> Get()
        {
            try
            {
                return _context.Stores.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// gets specific store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Store Get(int id) {
            try {
                return _context.Stores.Where(s => s.StoreID == id).First();
            }
            catch (Exception e) {
                throw e;
            }
        }
    }
}
