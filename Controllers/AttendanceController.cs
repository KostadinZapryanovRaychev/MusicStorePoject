using Microsoft.AspNetCore.Mvc;
using MvcMusicStoreWebProject.Data;
using MvcMusicStoreWebProject.Models;
using MvcMusicStoreWebProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Controllers
{
    public class AttendanceController : Controller
    {
        // tva teq raboti traq da si napravq truda da gi izucha nachi tuka imame konstruktor koito vzema IRepository kato argument dependancy injection i drugite neshta traq da gi razbera.
        private IRepository Repo { get; }
        public AttendanceController(IRepository repo)
        {
            Repo = repo;
        }

        // I tyk kak da go napravq da vrushta View to na sled deistviqta na vseki kontroler
        // pochvame da pravim kontroleri za vseki edin metod 
        [HttpGet]
        public IActionResult GetAttendances()
        {
            var attend = Repo.GetAttendances();
            return View(attend);
        }

        [HttpGet]
        public IActionResult GetAttendance(int id)
        {
            var at = Repo.GetAttendance(id);

            if (at != null)
            {
                return View(at);
            }
            return NotFound();
        }

        [HttpGet]

        public IActionResult CreateAttendance()
        {
            var viewModel = new AttendanceViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttendance(AttendanceViewModel attendanceViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                 
                    await Repo.AddAttendance(attendanceViewModel.Attendance);
                    return RedirectToAction("CreateAttendance");
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return Content("Exception happend");
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteAttendance(int AttendanceId)

        {
            var deleteMessage = await Repo.DeleteAttendance(AttendanceId);


            return Content(deleteMessage);

        }

        //[HttpDelete]
        //public async Task <IActionResult> DeleteAttendance(int id )

        //{
        //    var attend = await Repo.GetAttendance(id);

        //    if (attend != null)
        //    {
        //        await Repo.DeleteAttendance(attend);
        //        return Ok();
        //    }
        //    return NotFound();

        //}

        [HttpGet]
        public async Task<IActionResult> EditAttendance(int AttendanceId)

        {
            var existingAttend = await Repo.GetAttendance(AttendanceId);
            var viewModel = new AttendanceViewModel { Attendance = existingAttend };
            return View(viewModel);


        }

        [HttpPost]
        public async Task<IActionResult> EditAttendance(AttendanceViewModel modifiedAttendance)

        {
            if (ModelState.IsValid)
            {
                var existingAttend = modifiedAttendance.Attendance;
                await Repo.EditAttendance(existingAttend);
            }
            return View(modifiedAttendance);
        }

        public IActionResult Index()
        {
            IEnumerable<Attendance> attendances = Repo.GetAttendances();
            return View(attendances);
        }
    }
}
