using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using MvcMusicStoreWebProject.Data.Models;
using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// DbContextra mi veche nasledqva IdentityDbContext dvata klasa koite sa sazdadeni User , Role
namespace MvcMusicStoreWebProject.Data
{
    public class MusicStoreDbContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, int>
    {
        public MusicStoreDbContext(DbContextOptions<MusicStoreDbContext> options) : base (options)
        {

        }


        //public DbSet <Album> Albums { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<Programs> Programs { get; set; }

        public DbSet<Attendance> Attendances { get; set; }
    }
}
