using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStoreWebProject.Data;
using MvcMusicStoreWebProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Controllers
{
    public class UserController : Controller
    {
        private IAttendanceRepository Repo { get; }

        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(IAttendanceRepository repo, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            Repo = repo;
        }

        public async Task <string> CurrentUser()
        {


            Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
            var user = await GetCurrentUserAsync();
            var userEmail = user.Email;
            return userEmail;

            
        }
    }
}
