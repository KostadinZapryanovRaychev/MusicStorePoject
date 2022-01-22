using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMusicStoreWebProject.Data;
using MvcMusicStoreWebProject.Models;
using MvcMusicStoreWebProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Controllers
{
    public class DisciplineController : Controller
    {
        private IDisciplineRepository Repo { get; }

        private IRepository _Repo { get; }

        public List<Degrees> GetAllDegrees()
        {
            List<Degrees> degreesNames = _Repo.LetGetDegrees().ToList();
            return degreesNames;

        }
        public DisciplineController(IDisciplineRepository repo, IRepository repository)
        {
                Repo = repo;
                _Repo = repository;
        }

        // tova mi e za filtera
        public IActionResult Index(int ProgramsId)
        {
            ViewBag.degreesNames = new SelectList(GetAllDegrees(), "Id", "Name");

            IEnumerable<Discipline> disciplines;
            if (ProgramsId > 0)
            {
                disciplines = Repo.FindDisciplineByProgramsId(ProgramsId);
            }
            else
            {
                disciplines = Repo.GetAllAvailableDisciplines();
            }

            return View(disciplines);
        }

        //public IActionResult FilteredDisciplines(int ProgramsId)
        //{
        //    //tuk nqkak si kato izbere ot drop downa trqbva da vzemem tva value 
        //    ViewBag.programsNames = new SelectList(GetAllPrograms(), "Id", "Name");
        //    //i da go pasnem tuka kato ProgramsId paramatur
        //    IEnumerable<Discipline> disciplines = Repo.FindDisciplineByProgramsId(ProgramsId);
        //    return View(disciplines);
        //}


        [HttpGet]
        public async Task<IActionResult> EditDiscipline(int DisciplineId)

        {
            var existingDiscipline = await Repo.GetDiscipline(DisciplineId);
            var viewModel = new DisciplineViewModel { Discipline = existingDiscipline };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditDiscipline(DisciplineViewModel modifiedDiscipline)

        {
            if (ModelState.IsValid)
            {
                var existingDiscipline = modifiedDiscipline.Discipline;
                await Repo.EditDiscipline(existingDiscipline);
            }
            return View(modifiedDiscipline);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteDiscipline(int DisciplineId)
        {
            var deleteMessage = await Repo.DeleteDisciplineById(DisciplineId);
            return Content(deleteMessage);
        }


        [HttpGet]

        public IActionResult CreateDiscipline()
        {
            var viewModel = new DisciplineViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscipline(DisciplineViewModel disciplineViewModel)
        {
            if (ModelState.IsValid)
            {
                await Repo.AddDiscipline(disciplineViewModel.Discipline);
                return RedirectToAction("CreateDiscipline");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetDiscipline(int id)
        {
            var dis = Repo.GetDiscipline(id);

            if (dis != null)
            {
                return View(dis);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult GetDisciplines()
        {
            var disciplines = Repo.GetAllAvailableDisciplines();
            return View(disciplines);
        } 

    }
}
