using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStoreWebProject.Data;
using MvcMusicStoreWebProject.Data.Models;
using MvcMusicStoreWebProject.Models;
using MvcMusicStoreWebProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin.Host.SystemWeb;


namespace MvcMusicStoreWebProject.Controllers
{
   
    public class AttendanceController : Controller
    {
        // tva teq raboti traq da si napravq truda da gi izucha nachi tuka imame konstruktor koito vzema IRepository kato argument dependancy injection i drugite neshta traq da gi razbera.
        private IRepository Repo { get; }
        
        private readonly UserManager<ApplicationUser> _userManager;
        public AttendanceController(IRepository repo , UserManager <ApplicationUser> userManager)
        {
            _userManager = userManager;
            Repo = repo;
        }
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
        public IActionResult GetAttendanceById(int ApplicationUserId)
        {
            var at = Repo.GetAttendanceByUserId(ApplicationUserId);

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
            
                if (ModelState.IsValid)
                {

                    Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
                    var user = await GetCurrentUserAsync();
                    var userId = user.Id;
                    attendanceViewModel.Attendance.ApplicationUserId = userId;
                    await Repo.AddAttendance(attendanceViewModel.Attendance);
                    
                    return RedirectToAction("CreateAttendance");
                }

                return RedirectToAction("Index");
            

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



        public async Task <IActionResult> LoggedUser()
        {

            //IEnumerable<Attendance> attendances = Repo.GetAttendances();
            Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
            var user = await GetCurrentUserAsync();
            var userId = user.Id;
            IEnumerable<Attendance> attendances = Repo.FindAttendanceByUserId(userId);
            var result = attendances.OrderBy(x => x.Date);

            return View(result);
            

            //return View();
            //return View(attendances);
        }


        public IActionResult Index()
        {
            
            return View();
        }

        
        public async Task<IActionResult> Report()
        {

            //IEnumerable<Attendance> attendances = Repo.GetAttendances();
            Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
            var user = await GetCurrentUserAsync();
            var userId = user.Id;
            IEnumerable<Attendance> attendances = Repo.FindAttendanceByUserId(userId);
            var result = attendances.OrderBy(x => x.Date);
            return View(result);
            //return View(attendances);
        }


        // TOVA TRQBVA DA SE IZSLEDVA ZASHTO NE VLIZA TUKA IZOBSHTO spored men zaradi purvite Responsi koito dobavqhme   
        //public async Task<IActionResult> Multiply()
        //{

        //    Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        //    var user = await GetCurrentUserAsync();
        //    var userId = user.Id;
        //    IEnumerable<Attendance> attendances = Repo.FindAttendanceByUserId(userId);
        //    var result = attendances.OrderBy(x => x.Date);

        //    foreach (var r in result)
        //    {
        //        for (int i = 2; i < 4; i++)
        //        {
        //            Repo.Detached(r);
        //            r.Date.AddDays(7);
        //            await Repo.AddAttendance(r);

        //        }

        //    }
        //    return View();

        //}


        public async Task<IActionResult> Mulplication()
        {

            Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
            var user = await GetCurrentUserAsync();
            var userId = user.Id;
            IEnumerable<Attendance> attendances = Repo.FindAttendanceByUserId(userId);
            var result = attendances.OrderBy(x => x.Date);

            foreach (var r in result)
            {
                for (int i = 0; i < 3; i++)
                {
                    Repo.Detached(r);
                    r.Date =r.Date.AddDays(7);
                    // edin If statemenet za da filtrira Holidays // TOZi metod e interesen trqbva da go dopogledna
                    await Repo.AddAttendanceWithoutHolidays(r);

                }

            }
            return View();
        }
    }
}
