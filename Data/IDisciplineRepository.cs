using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Data
{
    public interface IDisciplineRepository
    {
        IEnumerable<Discipline> GetAllAvailableDisciplines();

        Task<Discipline> GetDiscipline(int id);

        Task<Discipline> EditDiscipline(Discipline discipline);

        Task<string> DeleteDiscipline(Discipline discipline);

        Task<string> DeleteDisciplineById(int id);

        Task<string> AddDiscipline(Discipline discipline);

        IEnumerable<Discipline> FindDisciplineByProgramsId(int ProgramsId);

        void Save();
    }
}
