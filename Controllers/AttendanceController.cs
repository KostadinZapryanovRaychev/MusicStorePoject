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
using RestSharp;
using System.Net;
using MvcMusicStoreWebProject.Areas.Identity.Pages.Account;

namespace MvcMusicStoreWebProject.Controllers
{

    public class AttendanceController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private IAttendanceRepository Repo { get; }

        private readonly UserManager<ApplicationUser> _userManager;
        public AttendanceController(IAttendanceRepository repo, UserManager<ApplicationUser> userManager , IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            Repo = repo;
            this._httpContextAccessor = httpContextAccessor;
        }



        public List<Semester> GetAllSemesterDisplay()
        {
            List<Semester> semNames = Repo.GetAllSemesters().ToList();
            return semNames;
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
                // tuk nadminavam sebe si
                //attendanceViewModel.Attendance.SemesterId = currentSem.Id;
                await Repo.AddAttendance(attendanceViewModel.Attendance);

                return RedirectToAction("LoggedUser");
            }

            return RedirectToAction("LoggedUser");

        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteAttendance(int AttendanceId)

        {
            var deleteMessage = await Repo.DeleteAttendance(AttendanceId);
            return Content(deleteMessage);

        }


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


        [Authorize]
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


            TempData.Keep();


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
            // ZNACHI TUK
            //var currentSelectedSemester = Repo.GetCurrentSemester();
            //var currentSelectedSemester = Request.Cookies["SemesterId"].Value;
            //HttpCookie cookie = HttpContext.Current.Request.Cookies[SemesterId];

            // E TUK NADNIMAH SEBE SI S TVA IZVRASHTENIE 
            var currentSelectedSemester = int.Parse(_httpContextAccessor.HttpContext.Request.Cookies["SemesterId"]);

            //var currSelectedSemester = Request.Cookies["SemesterId"].Value;

            //if (currentSelectedSemester != null)
            //{
            //    // proverqvame i displaivame zapisite za dadeniq semester
            //    attendances = Repo.FindAttendanceBySemesterIdandUserId(userId, currentSelectedSemester.Id, mode);
            //}

            if (currentSelectedSemester != null)
            {
                // proverqvame i displaivame zapisite za dadeniq semester
                attendances = Repo.FindAttendanceBySemesterIdandUserId(userId, currentSelectedSemester, mode);
            }
            else
            {
                attendances = Repo.FindAttendanceByUserId(userId, mode);
            }

            var result = attendances.OrderBy(x => x.Date);

            return View(result);
        }

        //public IActionResult Index()
        //{

        //    ViewBag.semNames = new SelectList(GetAllSemesterDisplay(), "Id", "Name");
        //    return View();
        //}



        [Authorize]
        public async Task<IActionResult> MultiplicationByAttendanceId(int AttendanceId, int MultiplicationLength)
        {
            
            var length = MultiplicationLength;
            var attendance = await Repo.GetAttendance(AttendanceId);
            var semester = Repo.SemesterEndDateById(attendance.SemesterId);


            for (int i = 0; i <= length; i++)
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
        public async Task<IActionResult> Multiplication(string mode = "редовно")
        {
            // tuk trqbva da se dobavi if 
            Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
            var user = await GetCurrentUserAsync();
            var userId = user.Id;
            var currentSelectedSemester = int.Parse(_httpContextAccessor.HttpContext.Request.Cookies["SemesterId"]);
            IEnumerable<Attendance> attendances;


            if (currentSelectedSemester != null)
            {
                // proverqvame i displaivame zapisite za dadeniq semester
                attendances = Repo.FindAttendanceBySemesterIdandUserId(userId, currentSelectedSemester, mode).ToList();
            }
            else
            {
                attendances = Repo.FindAttendanceByUserId(userId, mode).ToList();
            }

            foreach (var attendance in attendances)
            {
                var semesterEndDate = Repo.SemesterEndDateById(attendance.SemesterId);

                //for (int i = 0; i < 14; i++)
                for (int i = 0; i < 3; i++)
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
            return RedirectToAction("LoggedUser");
        }



        [Authorize]
        public async Task<IActionResult> UserHistory(string mode = "")
        {
            
            var currentSelectedSemester = int.Parse(_httpContextAccessor.HttpContext.Request.Cookies["SemesterId"]);
            //var selection = ViewBag.customerSelectedSemester;
            var selection = _httpContextAccessor.HttpContext.Request.Cookies["SemesterId"];
            ViewBag.selectedValue = selection;
            Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
            var user = await GetCurrentUserAsync();
            var userId = user.Id;
            var result = Repo.FindAttendanceBySemesterIdandUserId(userId, currentSelectedSemester, mode);
            return View(result);
        }

        [Authorize]
        public IActionResult WriteCokie(int SemesterId , string semName)
        {
            
            TempData["semesterName"] = semName;
            TempData.Keep(semName);
            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("SemesterId", SemesterId.ToString());
            return RedirectToAction("LoggedUser");
        }


        public List<string> AllowedNames { get; set; } = new List<string>()
        {
            new string ("Георги Иванов Георгиев"),
            new string ("Цветан Иванов Георгиев"),
            new string ("Михаил Иванов Георгиев"),
            new string ("Трендафил Иванов Георгиев"),
            new string ("Божидар Иванов Георгиев"),
            new string ("Манол Иванов Георгиев"),
            new string ("Костадин Иванов Георгиев"),
        };

        public bool isUserAllowedToRegister(RegisterModel registerModel)
        {

            if (!AllowedNames.Contains(registerModel.Input.OfficialName))
            {
                return true;
            }
            return false;
        }

    }
}
