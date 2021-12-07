using Microsoft.AspNetCore.Identity;
using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Data.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
