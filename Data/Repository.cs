using Microsoft.Extensions.Configuration;
using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMusicStoreWebProject.Models.ViewModels;
using MvcMusicStoreWebProject.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace MvcMusicStoreWebProject.Data
{

    public class Repository : IRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private MusicStoreDbContext Context { get; }
        public IConfiguration Config { get; }
        private string DeleteMassage { get; set; }
        private string ExceptionMessage { get; set; }

        //var NWD = new List<DateTime>()
        //{
        //    "2008, 6, 1, 7, 47, 0",
        //    ,
        //};

        

        public static bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday
                || date.DayOfWeek == DayOfWeek.Sunday;
        }

        // nqkvi shano ekvilibristiki
        public DateTime IsHoliday(NonWorkingDays nonWorkingDays)
        {
            var NOD = nonWorkingDays.Holiday;
            return NOD;
        }


        //public static int Compare(Attendance t1, NonWorkingDays t2)
        //{
        //    foreach (var d in t1.Date)
        //    {

        //    }
        //}


        public Repository(MusicStoreDbContext context, IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            Context = context;
            Config = config;
            ExceptionMessage = config.GetSection("RepositoryExceptionMassage").Value;

        }
        public async Task<string> AddAttendance(Attendance attendance)
        {

            Context.Attendances.Add(attendance);
            await Context.SaveChangesAsync();
            return "Inserted";
        }


        // tuk kva e razlikata dali shte go narpavq taka ili s ID nemoga da razbera
        public async Task<string> DeleteAttendance(int id)
        {
            Attendance deletedAttendance = await Context.Attendances.FindAsync(id);
            if (deletedAttendance != null)
            {
                Context.Attendances.Remove(deletedAttendance);
                await Context.SaveChangesAsync();
                DeleteMassage = "Removed succesfully";
            }

            else
            {
                DeleteMassage = "Something went wrong , try again";
            }

            return DeleteMassage;

        }

        public async Task<string> DeleteAttendance(Attendance attendance)
        {
            return await DeleteAttendance(attendance.Id);
        }

        public async Task<Attendance> EditAttendance(Attendance attendance)
        {
            var existingAttend = await Context.Attendances.FindAsync(attendance.Id);

            if (existingAttend != null)
            {
                existingAttend.Hours = attendance.Hours;
                existingAttend.Groupe = attendance.Groupe;
                existingAttend.Type = attendance.Type;
                existingAttend.Mode = attendance.Mode;
                existingAttend.Timeframe = attendance.Timeframe;
                existingAttend.Programs = attendance.Programs;
                existingAttend.Auditorium = attendance.Auditorium;
                existingAttend.Degree = attendance.Degree;
                existingAttend.Note = attendance.Note;
                existingAttend.Course = attendance.Course;

                Context.Attendances.Update(existingAttend);
                await Context.SaveChangesAsync();
            }
            return attendance;

        }

      


        // ei tva trqbva da listne vsichki attendances
        public IEnumerable<Attendance> GetAttendances()
        {
            return Context.Attendances.ToList();
        }

        public IEnumerable<Attendance> FindAttendanceByUserId(int ApplicationUserId)
        {

            return Context.Attendances.Where(a => a.ApplicationUserId == ApplicationUserId);
        }

        public async Task<List<ProgramsViewModel>> GetPrograms()
        {
            return await Context.Programs.Select(x => new ProgramsViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        public async Task<List<DisciplineViewModel>> GetDiscipline()
        {
            return await Context.Disciplines.Select(x => new DisciplineViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        public async Task<Attendance> GetAttendanceByUserId(int ApplicationUserId)
        {
            // ako id to vkarano kato parametar v taq funkciq e ravno na Id na Attendance vurni mi toq attendance !
            // return Context.Attendances.SingleOrDefault(x => x.Id == id);

            // durgiq variant e 

            var attend = await Context.Attendances.FindAsync(ApplicationUserId);
            return attend;
        }

        public async Task<Attendance> GetAttendance(int id)
        {
            // ako id to vkarano kato parametar v taq funkciq e ravno na Id na Attendance vurni mi toq attendance !
            // return Context.Attendances.SingleOrDefault(x => x.Id == id);

            // durgiq variant e 

            var attend = await Context.Attendances.FindAsync(id);
            return attend;
        }

        public void Detached(Attendance attendanceEntity)
        {
            // da se napravi novo Entity koeto da e kopie na tova i na nego da se sloji statusa detached
            Context.Entry(attendanceEntity).State = EntityState.Detached;
            attendanceEntity.Id = 0;
        }

        // primerno da deklarirame tuka nov AddAttendanceWithoutHolidays metod koito da pravi sushtoto kato gorniq no da chekva predi tva da ne bi da e holiday
        // mai bachkaaa mainaaa
        // mai bachkaaa mainaaa
        public async Task<string> AddAttendanceWithoutHolidays(Attendance attendance)
        {
            // tuka nqkuv if statement ili while cycle , NonWorkingDays nonWorkingDays

            var holidays = Context.NonWorkingDays.ToList();
            foreach (var h in holidays)
            {
                if (h.Holiday != attendance.Date)
                {
                    Context.Attendances.Add(attendance);
                    await Context.SaveChangesAsync();
                    return "Inserted";
                }
                return "!";
            }
            return "Success";
        }
    }
}
