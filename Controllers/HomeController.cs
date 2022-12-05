using GameTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameTracker.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }



    }
}