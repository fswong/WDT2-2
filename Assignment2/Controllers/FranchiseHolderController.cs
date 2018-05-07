using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Data;
using Assignment2.Models;
using Assignment2.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class FranchiseHolderController : Controller
    {
        #region properties
        private readonly ApplicationDbContext _context;
        #endregion

        #region ctor
        public FranchiseHolderController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Dashboard/FranchiseHolderDashboard.cshtml");
        }

        [HttpGet]
        public IActionResult Inventory(int id) {
            try {
                var model = _context.StoreInventories.Include(s => s.Product).Include(s => s.Store).Where(s => s.StoreID == id).ToList();
                return View(model);
            }
            catch (Exception e) {
                throw e;
            }
        }
    }
}