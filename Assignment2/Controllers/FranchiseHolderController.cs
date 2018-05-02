using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}