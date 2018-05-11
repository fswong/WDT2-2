using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Data;
using Assignment2.Models;
using Assignment2.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private int _StoreID;
        #endregion

        #region ctor
        public FranchiseHolderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        //public FranchiseHolderController(ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            var user = _userManager.GetUserId(User);
            var theUser = _context.Users.Where(u => u.Id == user).First();
            _StoreID = theUser.StoreID;
            //_StoreID = 1;
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
                
                var model = _context.StoreInventories.Include(s => s.Product).Include(s => s.Store).Where(s => s.StoreID == _StoreID).ToList();
                return View(model);
            }
            catch (Exception e) {
                throw e;
            }
        }
    }
}