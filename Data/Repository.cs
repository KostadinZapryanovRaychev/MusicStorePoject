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


        public List<DateTime> FreeDates { get; set; } = new List<DateTime>()
        {
            new DateTime(2021, 12, 21),
            new DateTime(2021, 12, 22),
            new DateTime(2021, 12, 23),
            new DateTime(2021, 12, 24),
            new DateTime(2021, 12, 25),
            new DateTime(2021, 12, 26),
            new DateTime(2021, 12, 27),
            new DateTime(2021, 12, 28)
        };

        public static bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday
                || date.DayOfWeek == DayOfWeek.Sunday;
        }



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

        public async Task<string> CoppyAttendance(Attendance attendance)
        {
          

            var originalEntity = Context.Attendances.AsNoTracking()
                             .FirstOrDefault(e => e.Id == attendance.Id);
            Context.Attendances.Add(originalEntity);
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
                existingAttend.Semester = attendance.Semester;
                existingAttend.SemesterId = attendance.SemesterId;
                existingAttend.Discipline = attendance.Discipline;
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


        // teq dvete po otdelno li ili da gi obedenq v edna ama mai po otdelno e po dobre
        public IEnumerable<Attendance> FindAttendanceBySemesterId(int SemesterId)
        {
            return Context.Attendances.Where(a => a.SemesterId == SemesterId);
        }

        public IEnumerable<Attendance> FindAttendanceBySemesterIdandUserId(int ApplicationUserId, int SemesterId)
        {
            return Context.Attendances.Where(a => a.ApplicationUserId == ApplicationUserId && a.SemesterId== SemesterId);
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

        public async Task<Attendance> GetAttendanceBySemesterId(int ApplicationUserId,int SemesterId)
        {
            // ako id to vkarano kato parametar v taq funkciq e ravno na Id na Attendance vurni mi toq attendance !
            // return Context.Attendances.SingleOrDefault(x => x.Id == id);

            // durgiq variant e 

            var attend = await Context.Attendances.FindAsync( ApplicationUserId , SemesterId);
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

        public void ReDetached(Attendance attendanceEntity)
        {
            // da se napravi novo Entity koeto da e kopie na tova i na nego da se sloji statusa detached
            Context.Entry(attendanceEntity).State = EntityState.Detached;
            attendanceEntity.Id = 0;
        }

        public async Task<string> AddAttendanceWithoutHolidays(Attendance attendance )
        {
            // вземаме от контекста таблицата с НонУъркингДейс

            var result = Context.NonWorkingDays.ToList();

            // правим нов лист от тип Datetime
            var holidays = new List<DateTime>();

            // попълваме листа с данните от таблицата НонУъркингДейс
            foreach (var holiday in result)
            {

                holidays.Add(holiday.Holiday);
            };

            // сравняваме дните в двете таблици и препокритите от NonWorkingDays се премахват като записи
            if (!holidays.Contains(attendance.Date))

                // тва беше със обикновен лист от данни
            //if (!FreeDates.Contains(attendance.Date))
            {
                Context.Attendances.Add(attendance);
                await Context.SaveChangesAsync();
                return "Inserted";
            }
            return "Success";
            
        }


        public IList<Discipline> LetGetDisciplines()
        {
            var disciplineslist = from Discipline in Context.Disciplines select Discipline;
            var disciplinenames = disciplineslist.ToList<Discipline>();
            return disciplinenames;
        }

        public IList<Degrees> LetGetDegrees()
        {
            var degreeslist = from Degrees in Context.Degrees select Degrees;
            var degreenames = degreeslist.ToList<Degrees>();
            return degreenames;
        }


        public IList<Discipline> GetDisciplinesByProgramId(int DegreesId)
        {
            var disciplinelist = from discipline in Context.Disciplines where discipline.DegreesId == DegreesId select discipline;
            var disciplineNames = disciplinelist.ToList<Discipline>();
            var onlyNames = new List<string>();

            foreach (var onlyName in disciplineNames)
            {

                onlyNames.Add(onlyName.Name);
            };

            return disciplineNames;
        }

        public async Task<List<SemesterViewModel>> GetSemester()
        {
            return await Context.Semesters.Select(x => new SemesterViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }








        //trqbva da se opravi zaduljitelno imeto na View Modela osobeno na gornata funkciq
        public async Task<List<DegreeViewModel>> GetDegrees()
        {
            return await Context.Degrees.Select(x => new DegreeViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }


        public IList<Semester> GetAllSemesters()
        {
            var allSemesters = from semester in Context.Semesters select semester;
            var allSemestersNames = allSemesters.ToList<Semester>();
            return allSemestersNames;
        }

        public DateTime GetRelatedSemester (int id)
        {
            var semester = Context.Semesters.FirstOrDefault(x => x.Id == id);
            return semester.endDate;
        }


        // tva tuka вземаме големината на семестъра
        public int GetRelatedSemesterLongitude(int id)
        {
            var semester = Context.Semesters.FirstOrDefault(x => x.Id == id);
            var semesterLongitude= ((semester.startDate -semester.endDate)/7).Days;
            return semesterLongitude;
        }

    }
}
