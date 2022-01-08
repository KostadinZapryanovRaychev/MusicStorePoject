using Microsoft.AspNetCore.Identity;
using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Data.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public byte[] UserPhoto { get; set; }

        public string OfficialName { get; set; }
        public string Description { get; set; }

    }
}

