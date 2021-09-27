using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using MvcMusicStoreWebProject.Data.Models;
using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Data
{
    public class MusicStoreDbContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, int>
    {
        public MusicStoreDbContext(DbContextOptions<MusicStoreDbContext> options) : base (options)
        {

        }


        public DbSet <Album> Albums { get; set; }
    }
}
