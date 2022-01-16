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
using Microsoft.AspNetCore.Mvc.Rendering;

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


        public JsonResult GetDisciplineByProgramId(int DegreesId)
        {
            var disciplines = Repo.GetDisciplinesByProgramId(DegreesId);
            return Json(new SelectList(disciplines, "Id", "Name"));
        }


        public List<Degrees> GetAllDegrees()
        {
            List<Degrees> degreeNames = Repo.LetGetDegrees().ToList();
            return degreeNames;

        }

        public List<Discipline> GetAllDisciplines()
        {
            List<Discipline> discPlineNames = Repo.LetGetDisciplines().ToList();
            return discPlineNames;
           
        }
       

        [HttpGet]

        public IActionResult CreateAttendance()
        {
            ViewBag.degreesNames = new SelectList(GetAllDegrees(), "Id", "Name");
            ViewBag.semNames = new SelectList(GetAllSemesterDisplay(), "Id", "Name");
            var viewModel = new AttendanceViewModel(); 
            
            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> CreateAttendance(AttendanceViewModel attendanceViewModel   )
        {
            ViewBag.degreesNames = new SelectList(GetAllDegrees(), "Id", "Name");
            ViewBag.semNames = new SelectList(GetAllSemesterDisplay(), "Id", "Name");


            attendanceViewModel.Attendance.Degree = GetAllDegrees().Where(x => x.Id == attendanceViewModel.DegreeId).Select(x => x.Name).FirstOrDefault();
            //attendanceViewModel.Attendance.Discipline = GetDisciplineByProgramId(DegreesId).Where(x => x.Id == attendanceViewModel.DegreeId).Select(x => x.Name).FirstOrDefault();

            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();
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
            ViewBag.degreesNames = new SelectList(GetAllDegrees(), "Id", "Name");
            ViewBag.semNames = new SelectList(GetAllSemesterDisplay(), "Id", "Name");
            var existingAttend = await Repo.GetAttendance(AttendanceId);
            var viewModel = new AttendanceViewModel { Attendance = existingAttend };
            return View(viewModel);


        }


        [HttpPost]
        public async Task<IActionResult> EditAttendance(AttendanceViewModel modifiedAttendance)

        {
            ViewBag.degreesNames = new SelectList(GetAllDegrees(), "Id", "Name");
            ViewBag.semNames = new SelectList(GetAllSemesterDisplay(), "Id", "Name");

            modifiedAttendance.Attendance.Degree = GetAllDegrees().Where(x => x.Id == modifiedAttendance.DegreeId).Select(x => x.Name).FirstOrDefault();
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
            ViewBag.semNames = new SelectList(GetAllSemesterDisplay(), "Id", "Name");
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

        //public async Task<IActionResult> Multiplication()
        //{
        //    Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        //    var user = await GetCurrentUserAsync();
        //    var userId = user.Id;
        //    // da vzema oshte 2 parametura start date i end date 
        //    IEnumerable<Attendance> attendances = Repo.FindAttendanceByUserId(userId);
        //    var result = attendances.OrderBy(x => x.Date);

        //    foreach (var r in result)
        //    {
        //        var semester = Repo.GetRelatedSemester(r.SemesterId);

        //        for (int i = 0; i < 3; i++)
        //        {
        //            var newAttendanceDate = r.Date.AddDays(7);
        //            if (newAttendanceDate < semester)
        //            {
        //                Repo.Detached(r);
        //                r.Date = newAttendanceDate;
        //                // edin If statemenet za da filtrira Holidays // TOZi metod e interesen trqbva da go dopogledna
        //                await Repo.AddAttendanceWithoutHolidays(r);

        //            }
        //        }

        //    }
        //    return View();
        //}

        public async Task<IEnumerable<Attendance>> AttendanceByUserId()
        {
            Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
            var user = await GetCurrentUserAsync();
            var userId = user.Id;
            IEnumerable<Attendance> attendances = Repo.FindAttendanceByUserId(userId);
            var result = attendances.OrderBy(x => x.Date);
            return result;
        }

        public async Task<IActionResult> Multiplication()
        {
            var attendence =await AttendanceByUserId();
            foreach (var r in attendence)
            {
                var semester = Repo.GetRelatedSemester(r.SemesterId);
                //var semmesterLenght = Repo.GetRelatedSemesterLongitude(r.SemesterId);

                for (int i = 0; i < 14; i++)
                {
                    var newAttendanceDate = r.Date.AddDays(7);
                    if (newAttendanceDate < semester) 
                    {
                        Repo.Detached(r);
                        r.Date = newAttendanceDate;
                        // edin If statemenet za da filtrira Holidays // TOZi metod e interesen trqbva da go dopogledna
                        await Repo.AddAttendanceWithoutHolidays(r);

                    }
                }

            }
            return View();
        }

        public async Task<IActionResult> UserHistory(int SemesterId)
        {
            ViewBag.semNames = new SelectList(GetAllSemesterDisplay(), "Id", "Name");
            Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
            var user = await GetCurrentUserAsync();
            var userId = user.Id;
            var result = Repo.FindAttendanceBySemesterIdandUserId(userId, SemesterId);
            
            return View(result);
        }



        public List<Semester> GetAllSemesterDisplay()
        {
            List<Semester> semNames = Repo.GetAllSemesters().ToList();
            return semNames;

        }

    }
}
