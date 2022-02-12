using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMusicStoreWebProject.Models.ViewModels;

namespace MvcMusicStoreWebProject.Data
{

    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetAttendances();


        // spored men tuk kato parametar vutre trqbva da se nashie novoto Id koeto shte se vkluchi v tablicata
        Task<Attendance> GetAttendance(int Id);

        Task<string> AddAttendance(Attendance attendance);

        Task<string> DeleteAttendance(Attendance attendance);

        Task<string> DeleteAttendance(int Id);

        Task<Attendance> EditAttendance(Attendance attendance);

        Task<List<DisciplineViewModel>> GetDiscipline();

        Task<Attendance> GetAttendanceByUserId(int ApplicationUserId);

        public IEnumerable<Attendance> FindAttendanceByUserId(int ApplicationUserId , string mode = "");

        public void Detached(Attendance entity);


        // probvame da slojim tova vmesto AddAttendance
        //Task<string> AddAttendanceWithoutHolidays(Attendance attendance, int semesterId);

        Task<string> AddAttendanceWithoutHolidays(Attendance attendance);


        //Task<string> GetCurrentUserDetails();

        Task<List<DegreeViewModel>> GetDegrees();

        IList<Degrees> LetGetDegrees();

        IList<Discipline> LetGetDisciplines();

        IList<Semester> GetAllSemesters();

        Task<List<SemesterViewModel>> GetSemester();

        IList<Discipline> GetDisciplinesByProgramId(int DegreesId);

        DateTime SemesterEndDateById(int id);


        IEnumerable<Attendance> FindAttendanceBySemesterId(int SemesterId);

        int GetRelatedSemesterLongitude(int id);

        IEnumerable<Attendance> FindAttendanceBySemesterIdandUserId(int ApplicationUserId, int SemesterId , string mode = "");

        Task<string> CoppyAttendance(Attendance attendance);

        void ReDetached(Attendance attendanceEntity);

        Semester GetCurrentSemester();

        Task<Attendance> GetAttendanceByMode(string Mode);

    }
}