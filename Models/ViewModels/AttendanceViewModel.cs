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
         
        public int DegreeId { get; set; }

        public int DisciplineId { get; set; }

        public List<SelectListItem> AvailableTypes { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem {Text="Семинарно упражнение"},
            new SelectListItem {Text="Лабораторно упражнение"},
            new SelectListItem {Text="Лекция"}

        };

        public List<SelectListItem> AvailableModes { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem {Text="редовно" },
            new SelectListItem {Text="задочно" },
            

        };
    }
}
