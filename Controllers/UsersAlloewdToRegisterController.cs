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
    public class UsersAlloewdToRegisterController : Controller
    {

        private IAttendanceRepository _Repo { get; }

        private IAPtRRepository Repo { get; }
        public UsersAlloewdToRegisterController(IAPtRRepository repo, IAttendanceRepository repository)
        {
            Repo = repo;
            _Repo = repository;

        }
        public IActionResult Index()
        {
            var allowedNames = Repo.GetAllAvailableAPtoReg();
            return View(allowedNames);
        }

        [HttpGet]
        public async Task<IActionResult> EditAPtoReg(int APtoRegId)

        {
            var existingAPtoReg = await Repo.GetAPtoReg(APtoRegId);
            var viewModel = new AllowedPersonsToRegisterViewModel { AllowedPersonsToRegister = existingAPtoReg };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAPtoReg(AllowedPersonsToRegisterViewModel allowedPersonsToRegisterView)

        {
            if (ModelState.IsValid)
            {
                var existingARtoReg = allowedPersonsToRegisterView.AllowedPersonsToRegister;
                await Repo.EditAPtoReg(existingARtoReg);
            }
            return View(allowedPersonsToRegisterView);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteAPtoReg(int APtoRId)
        {
            var deleteMessage = await Repo.DeleteAPtoReg(APtoRId);
            return Content(deleteMessage);
        }


        [HttpGet]

        public IActionResult CreateAPtoReg()
        {
            var viewModel = new AllowedPersonsToRegisterViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAPtoReg(AllowedPersonsToRegisterViewModel allowedPersonsToRegisterViewModel)
        {
            if (ModelState.IsValid)
            {
                await Repo.AddAPtoReg(allowedPersonsToRegisterViewModel.AllowedPersonsToRegister);
                return RedirectToAction("CreateAPtoReg");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetAPtoReg(int id)
        {
            var person = Repo.GetAPtoReg(id);

            if (person != null)
            {
                return View(person);
            }
            return NotFound();
        }
    }
}
