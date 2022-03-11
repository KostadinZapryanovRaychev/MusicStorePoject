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
    public class SemesterRepository : ISemesterRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private MusicStoreDbContext Context { get; }
        public IConfiguration Config { get; }
        private string DeleteMassage { get; set; }
        private string ExceptionMessage { get; set; }
        public SemesterRepository(MusicStoreDbContext context, IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            Context = context;
            Config = config;
            ExceptionMessage = config.GetSection("RepositoryExceptionMassage").Value;

        }

        public async Task<string> AddSemester(Semester semester)
        {
            Context.Semesters.Add(semester);
            await Context.SaveChangesAsync();
            return "Inserted";
        }

        public async Task<string> DeleteSemesterById(int id)
        {
            Semester deletedSemester = await Context.Semesters.FindAsync(id);
            if (deletedSemester != null)
            {
                Context.Semesters.Remove(deletedSemester);
                await Context.SaveChangesAsync();
                DeleteMassage = "Removed succesfully";
            }

            else
            {
                DeleteMassage = "Something went wrong , try again";
            }

            return DeleteMassage;

        }


        public async Task<string> DeleteSemester(Semester semester)
        {
            return await DeleteSemesterById(semester.Id);
        }


        public async Task<Semester> EditSemester(Semester semester)
        {
            var existingSemester = await Context.Semesters.FindAsync(semester.Id);
            if (existingSemester != null)
            {
                existingSemester.Name = semester.Name;
                existingSemester.startDate = semester.startDate;
                existingSemester.endDate = semester.endDate;
                Context.Semesters.Update(existingSemester);
                await Context.SaveChangesAsync();
            }
            return semester;
        }


        public async Task<Semester> GetSemester(int id)
        {
            var semester = await Context.Semesters.FindAsync(id);
            return semester;
        }

        public IEnumerable<Semester> GetAllAvailableSemesters()
        {
            return Context.Semesters.ToList();
        }

        public async void Save()
        {
            await Context.SaveChangesAsync();
        }



    }
}
