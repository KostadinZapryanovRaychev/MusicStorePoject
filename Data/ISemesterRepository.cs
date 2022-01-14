using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Data
{
    public interface ISemesterRepository
    {
        Task<string> AddSemester(Semester semester);

        Task<string> DeleteSemesterById(int id);

        Task<string> DeleteSemester(Semester semester);

        Task<Semester> EditSemester(Semester semester);

        Task<Semester> GetSemester(int id);

        IEnumerable<Semester> GetAllAvailableSemesters();

        void Save();
    }
}
