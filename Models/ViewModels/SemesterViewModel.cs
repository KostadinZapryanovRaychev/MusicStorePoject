using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MvcMusicStoreWebProject.Models.ViewModels
{
    public class SemesterViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

    }
}
