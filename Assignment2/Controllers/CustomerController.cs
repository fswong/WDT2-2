using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Controllers
{
    [Authorize(Roles = Constants.CustomerRole)]
    [Route("[controller]/[action]")]
    public class CustomerController : Controller
    {
        #region properties
        private readonly ApplicationDbContext _context;
        #endregion

        #region ctor
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult Inventory(int id, int pageNumber = 1) {
            const int PageSize = 3;

            //ViewBag.PageNumber = pageNumber;
            //ViewBag.PageSize = PageSize;
            //ViewBag.TotalMovies = db.Movies.Count();

            //return View(db.Movies.OrderBy(movie => movie.Title).
            //            Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList());

//            < div class="pagerDiv">
//    Page @(pagedList.PageCount<pagedList.PageNumber? 0 : pagedList.PageNumber) of @pagedList.PageCount
//    @Html.PagedListPager(pagedList, page => Url.Action("Index", new { pageNumber = page
//    }))
//</div>

            var model = _context.StoreInventories.Include(si => si.Product).Include(si => si.Store).Where(si => si.StoreID == id).
                Skip((pageNumber - 1) * PageSize).Take(PageSize).
                ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Cart()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cart(int data)
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}