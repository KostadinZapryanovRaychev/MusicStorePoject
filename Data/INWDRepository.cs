using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Data
{
    public interface INWDRepository
    {
        IEnumerable<NonWorkingDays> FindNonWorkingDaysBySemesterId(int SemesterId);
        void Save();
        IEnumerable<NonWorkingDays> GetAllAvailableNWD();

        Task<NonWorkingDays> GetNWD(int id);

        Task<NonWorkingDays> EditNWD(NonWorkingDays nonWorkingDays);

        Task<string> DeleteNWD(NonWorkingDays nonWorkingDays);
        Task<string> DeleteNWD(int id);

        Task<string> AddNWD(NonWorkingDays nonWorkingDays); 

    }
}
