using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assignment2.Models;
using Assignment2.Data;

namespace Assignment2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // check session
            if (User.IsInRole(Constants.CustomerRole))
            {
                return RedirectToAction("Index", "Customer");
            }
            else if (User.IsInRole(Constants.FranchiseHolderRole))
            {
                return RedirectToAction("Inventory", "FranchiseHolder");
            }
            else if (User.IsInRole(Constants.OwnerRole))
            {
                return RedirectToAction("Index", "Owner");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
