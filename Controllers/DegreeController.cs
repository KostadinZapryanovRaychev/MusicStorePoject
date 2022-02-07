using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStoreWebProject.Data;
using MvcMusicStoreWebProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DegreeController : Controller
    {
        private IDegreeRepository Repo { get; }

        private IAttendanceRepository _Repo { get; }
        public DegreeController(IDegreeRepository repo, IAttendanceRepository repository)
        {
            Repo = repo;
            _Repo = repository;
        }

        public IActionResult Index()
        {
            var degrees = Repo.GetAllAvailableDegrees();
            // ot nai noviq kum nai stariq ailqche 
            var result = degrees.OrderByDescending(x => x.Id);
            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> EditDegrees(int DegreesId)

        {
            var existingDegree = await Repo.GetDegrees(DegreesId);
            var viewModel = new DegreeViewModel { Degrees = existingDegree };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditDegrees(DegreeViewModel modifiedDegree)

        {
            if (ModelState.IsValid)
            {
                var existingDegree = modifiedDegree.Degrees;
                await Repo.EditDegrees(existingDegree);
            }
            return View(modifiedDegree);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteDegree(int DegreesId)
        {
            var deleteMessage = await Repo.DeleteDegreesById(DegreesId);
            return Content(deleteMessage);
        }


        [HttpGet]

        public IActionResult CreateDegree()
        {
            var viewModel = new DegreeViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDegree(DegreeViewModel degreeViewModel)
        {
            if (ModelState.IsValid)
            {
                await Repo.AddDegrees(degreeViewModel.Degrees);
                return RedirectToAction("CreateDegree");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetDegree(int id)
        {
            var deg = Repo.GetDegrees(id);

            if (deg != null)
            {
                return View(deg);
            }
            return NotFound();
        }

    }
}
