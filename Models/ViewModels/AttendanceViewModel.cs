using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MvcMusicStoreWebProject.Models.ViewModels
{
    public class AttendanceViewModel
    {

        public Attendance Attendance { get; set; }

        public List<SelectListItem> AvailableDegrees { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem {Text="Bachelor" , Value="Bachelor"},
            new SelectListItem {Text="Master" , Value="Master"},
            new SelectListItem {Text="Doctor" , Value="Doctor"}

        };
    }
}
