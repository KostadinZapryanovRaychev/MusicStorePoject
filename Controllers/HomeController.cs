using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MvcMusicStoreWebProject.Data;
using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAttendanceRepository Repo { get; }

        public HomeController(ILogger<HomeController> logger , IAttendanceRepository repo)
        {
            _logger = logger;
            Repo = repo;
        }


        public List<Semester> GetAllSemesterDisplay()
        {
            List<Semester> semNames = Repo.GetAllSemesters().ToList();
            return semNames;
        }


        [Authorize]
        public IActionResult Index(string value)
        {
            ViewBag.semNames = new SelectList(GetAllSemesterDisplay(), "Id", "Name");
            //value = ViewBag.semNames();
            TempData["name"] = value;

            return View();
        }

        //public IActionResult Index2()
        //{
        //    // eee s taq tupa vrutka go prashtame kudeto taq iska 
        //    return RedirectToPage("/Account/Login", new { area = "Identity" });
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
