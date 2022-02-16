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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace MvcMusicStoreWebProject.Controllers
{

    public class AttendanceController : Controller
    {
        // tva teq raboti traq da si napravq truda da gi izucha nachi tuka imame konstruktor koito vzema IRepository kato argument dependancy injection i drugite neshta traq da gi razbera.
        private IAttendanceRepository Repo { get; }

        private readonly UserManager<ApplicationUser> _userManager;
        public AttendanceController(IAttendanceRepository repo, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            Repo = repo;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAttendances()
        {
            var attend = Repo.GetAttendances();
            return View(attend);
        }

        [Authorize]
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


        [Authorize]
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



        [Authorize]
        [HttpGet]

        public IActionResult CreateAttendance()
        {
            ViewBag.degreesNames = new SelectList(GetAllDegrees(), "Id", "Name");
            ViewBag.semNames = new SelectList(GetAllSemesterDisplay(), "Id", "Name");
            var viewModel = new AttendanceViewModel();

            return View(viewModel);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAttendance(AttendanceViewModel attendanceViewModel)
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
                // tuk sashto
                var currentSem = Repo.GetCurrentSemesterId();
                attendanceViewModel.Attendance.ApplicationUserId = userId;
                // tuk nadminavam sebe si
                attendanceViewModel.Attendance.SemesterId = currentSem.Id;
                await Repo.AddAttendance(attendanceViewModel.Attendance);

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");


        }


        [Authorize]
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


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditAttendance(int AttendanceId)

        {
            ViewBag.degreesNames = new SelectList(GetAllDegrees(), "Id", "Name");
            ViewBag.semNames = new SelectList(GetAllSemesterDisplay(), "Id", "Name");
            var existingAttend = await Repo.GetAttendance(AttendanceId);
            var viewModel = new AttendanceViewModel { Attendance = existingAttend };
            return View(viewModel);
        }

        [Authorize]
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



        public async Task<IActionResult> CoppyAttendance(int AttendanceId)

        {
            var existingAttend = await Repo.GetAttendance(AttendanceId);
            //var newAttendanceDate = existingAttend.Date.AddDays(1);
            //existingAttend.Date = newAttendanceDate;
            Repo.Detached(existingAttend);
            await Repo.AddAttendance(existingAttend);
            return RedirectToAction("LoggedUser");
        }


        [Authorize]
        public async Task<IActionResult> LoggedUser(string mode)
        {

            List<SelectListItem> mySkills = new List<SelectListItem>()
                    {
                                new SelectListItem {
                                    Text = "1", Value = "1"
                                },
                                new SelectListItem {
                                    Text = "2", Value = "2"
                                },
                                new SelectListItem {
                                    Text = "3", Value = "3"
                                },
                                new SelectListItem {
                                    Text = "4", Value = "4"
                                },
                                new SelectListItem {
                                    Text = "5", Value = "5"
                                },

                     };
            ViewBag.MySkills = mySkills;


            //IEnumerable<Attendance> attendances = Repo.GetAttendances();
            Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
            var user = await GetCurrentUserAsync();
            var userId = user.Id;
            IEnumerable<Attendance> attendances;

            // vzemame CurrentDate i sravnqvame dali Current date prisustva v nqkoi interval na start i endDate
            var currentSemester = Repo.GetCurrentSemester();
            if (currentSemester != null)
            {
                // proverqvame i displaivame zapisite za dadeniq semester
                attendances = Repo.FindAttendanceBySemesterIdandUserId(userId, currentSemester.Id, mode);
            }
            else
            {
                attendances = Repo.FindAttendanceByUserId(userId, mode);
            }

            var result = attendances.OrderBy(x => x.Date);

            return View(result);
        }

        public IActionResult Index()
        {

            ViewBag.semNames = new SelectList(GetAllSemesterDisplay(), "Id", "Name");
            return View();
        }



        [Authorize]
        public async Task<IActionResult> MultiplicationByAttendanceId(int AttendanceId, int MultiplicationLength)
        {
            //, int MultiplicationLenght

            var length = MultiplicationLength;
            var attendance = await Repo.GetAttendance(AttendanceId);
            var semester = Repo.SemesterEndDateById(attendance.SemesterId);


            // i < multiplicationLenght;

            for (int i = 0; i <= length; i++)
            //for (int i = 0; i < 2; i++)
            {
                var newAttendanceDate = attendance.Date.AddDays(1);
                if (newAttendanceDate < semester)
                {
                    Repo.Detached(attendance);
                    attendance.Date = newAttendanceDate;
                    // edin If statemenet za da filtrira Holidays // TOZi metod e interesen trqbva da go dopogledna
                    await Repo.AddAttendanceWithoutHolidays(attendance);

                }
            }
            return RedirectToAction("LoggedUser");
        }


        [Authorize]
        public async Task<IActionResult> CopyAttendance(Attendance att)
        {
            var attendence = await Repo.CoppyAttendance(att);

            return RedirectToAction("Index");
        }


        public async Task<IEnumerable<Attendance>> AttendanceByUserId()
        {
            Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
            var user = await GetCurrentUserAsync();
            var userId = user.Id;
            IEnumerable<Attendance> attendances = Repo.FindAttendanceByUserId(userId);
            var result = attendances.OrderBy(x => x.Date);
            return result;
        }


        public int GiveMeValue(int MultiplicationLenght)
        {

            List<SelectListItem> mySkills = new List<SelectListItem>()
                    {
                                new SelectListItem {
                                    Text = "1", Value = "1"
                                },
                                new SelectListItem {
                                    Text = "2", Value = "2"
                                },
                                new SelectListItem {
                                    Text = "3", Value = "3"
                                },
                                new SelectListItem {
                                    Text = "4", Value = "4"
                                },
                                new SelectListItem {
                                    Text = "5", Value = "5"
                                },

                     };
            ViewBag.MySkills = mySkills;
            var result = MultiplicationLenght;
            return result;
        }

        [Authorize]

        public async Task<IActionResult> MultiplicationByAttendanceIdForSemester(int AttendanceId)
        {


            var attendance = await Repo.GetAttendance(AttendanceId);
            var semester = Repo.SemesterEndDateById(attendance.SemesterId);


            // i < multiplicationLenght;

            for (int i = 0; i < 14; i++)
            {
                var newAttendanceDate = attendance.Date.AddDays(7);
                if (newAttendanceDate < semester)
                {
                    Repo.Detached(attendance);
                    attendance.Date = newAttendanceDate;
                    // edin If statemenet za da filtrira Holidays // TOZi metod e interesen trqbva da go dopogledna
                    await Repo.AddAttendanceWithoutHolidays(attendance);

                }
            }
            return RedirectToAction("Index");
        }



        [Authorize]
        public async Task<IActionResult> Multiplication(string mode = "редовно")
        {
            // tuk trqbva da se dobavi if 
            Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
            var user = await GetCurrentUserAsync();
            var userId = user.Id;
            var currentSemester = Repo.GetCurrentSemester();
            IEnumerable<Attendance> attendances;


            if (currentSemester != null)
            {
                // proverqvame i displaivame zapisite za dadeniq semester
                attendances = Repo.FindAttendanceBySemesterIdandUserId(userId, currentSemester.Id, mode).ToList();
            }
            else
            {
                attendances = Repo.FindAttendanceByUserId(userId, mode).ToList();
            }


            //attendances.OrderBy(x => x.Date);

            foreach (var attendance in attendances)
            {
                var semesterEndDate = Repo.SemesterEndDateById(attendance.SemesterId);
                //var semmesterLenght = Repo.GetRelatedSemesterLongitude(attendance.SemesterId);

                for (int i = 0; i < 14; i++)
                {
                    var newAttendanceDate = attendance.Date.AddDays(7);
                    if (newAttendanceDate < semesterEndDate)
                    {
                        Repo.Detached(attendance);
                        attendance.Date = newAttendanceDate;
                        // edin If statemenet za da filtrira Holidays // TOZi metod e interesen trqbva da go dopogledna
                        await Repo.AddAttendanceWithoutHolidays(attendance);

                    }
                }

            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UserHistory(int SemesterId, string mode = "всички")
        {
            ViewBag.semNames = new SelectList(GetAllSemesterDisplay(), "Id", "Name");
            var result = await UserHistoryChoice(SemesterId, mode);

            return View(result);
        }

        [Authorize]
        // kak da vurna sashtot view koeto e na UserHistory ama bez layout
        public async Task<IActionResult> Report(int SemesterId, string mode = "всички")
        {
            var selectList = new SelectList(GetAllSemesterDisplay(), "Id", "Name");
            ViewBag.Selected = SemesterId;
            ViewBag.semNames = selectList;
            var result = await UserHistoryChoice(SemesterId, mode);
            return View(result);
        }


        public async Task<IEnumerable<Attendance>> UserHistoryChoice(int SemesterId, string mode = "всички")
        {
            ViewBag.semNames = new SelectList(GetAllSemesterDisplay(), "Id", "Name");
            Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
            var user = await GetCurrentUserAsync();
            var userId = user.Id;
            return Repo.FindAttendanceBySemesterIdandUserId(userId, SemesterId, mode);
        }

        public IActionResult WriteCokie(int SemesterId)
        {
            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Append(SemesterId,cookies);
            return View();
        }

        public async Task<IActionResult> ReadCokie()
        {
            return View();
        }

        public List<Semester> GetAllSemesterDisplay()
        {
            List<Semester> semNames = Repo.GetAllSemesters().ToList();
            return semNames;
        }


        //////////////////////////////////////////////////////////Brutalni PROBi //////////////////////////////////////
        




    }
}
