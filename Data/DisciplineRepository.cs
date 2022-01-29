using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MvcMusicStoreWebProject.Data.Models;
using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Data
{
    public class DisciplineRepository: IDisciplineRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private MusicStoreDbContext Context { get; }
        public IConfiguration Config { get; }
        private string DeleteMassage { get; set; }
        private string ExceptionMessage { get; set; }
        public DisciplineRepository(MusicStoreDbContext context, IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            Context = context;
            Config = config;
            ExceptionMessage = config.GetSection("RepositoryExceptionMassage").Value;
        }


        public async Task<string> AddDiscipline(Discipline discipline)
        {
            Context.Disciplines.Add(discipline);
            await Context.SaveChangesAsync();
            return "Inserted";
        }

        public async Task<string> DeleteDisciplineById(int id)
        {
            Discipline deletedDiscipline = await Context.Disciplines.FindAsync(id);
            if (deletedDiscipline != null)
            {
                Context.Disciplines.Remove(deletedDiscipline);
                await Context.SaveChangesAsync();
                DeleteMassage = "Removed succesfully";
            }

            else
            {
                DeleteMassage = "Something went wrong , try again";
            }

            return DeleteMassage;

        }


        public async Task<string> DeleteDiscipline(Discipline discipline)
        {
            return await DeleteDisciplineById(discipline.Id);
        }


        public async Task<Discipline> EditDiscipline(Discipline discipline)
        {
            var existingDiscipline = await Context.Disciplines.FindAsync(discipline.Id);
            if (existingDiscipline != null)
            {
                existingDiscipline.Name = discipline.Name;
                existingDiscipline.DegreesId = discipline.DegreesId;
                Context.Disciplines.Update(existingDiscipline);
                await Context.SaveChangesAsync();
            }
            return discipline;
        }


        public async Task<Discipline> GetDiscipline(int id)
        {
            var discipline = await Context.Disciplines.FindAsync(id);
            return discipline;
        }

        public IEnumerable<Discipline> GetAllAvailableDisciplines()
        {
            return Context.Disciplines.ToList();
        }

        public async void Save()
        {
            await Context.SaveChangesAsync();
        }


        public IEnumerable<Discipline> FindDisciplineByProgramsId(int DegreesId)
        {
            return Context.Disciplines.Where(a => a.DegreesId == DegreesId);
        }
    }
}
