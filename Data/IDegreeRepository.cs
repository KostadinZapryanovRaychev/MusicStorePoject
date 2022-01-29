using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Data
{
    public interface IDegreeRepository
    {
        Task<string> AddDegrees(Degrees degrees);

        Task<string> DeleteDegreesById(int id);

        Task<string> DeleteDegrees(Degrees degrees);

        Task<Degrees> EditDegrees(Degrees degrees);

        Task<Degrees> GetDegrees(int id);

        IEnumerable<Degrees> GetAllAvailableDegrees();

        void Save();

    }
}
