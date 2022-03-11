using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using MvcMusicStoreWebProject.Data.Models;
using MvcMusicStoreWebProject.Models;
using MvcMusicStoreWebProject.Models.ViewModels;
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


        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<NonWorkingDays> NonWorkingDays { get; set; }
        public DbSet<Degrees> Degrees { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<AllowedPersonsToRegister> AllowedPersonsToRegisters { get; set; }

    }
}
