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
    public class NWDRepository : INWDRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private MusicStoreDbContext Context { get; }
        public IConfiguration Config { get; }
        private string DeleteMassage { get; set; }
        private string ExceptionMessage { get; set; }

        public NWDRepository(MusicStoreDbContext context, IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            Context = context;
            Config = config;
            ExceptionMessage = config.GetSection("RepositoryExceptionMassage").Value;
        }


        public async Task<string> AddNWD(NonWorkingDays nonWorkingDays)
        {
            Context.NonWorkingDays.Add(nonWorkingDays);
            await Context.SaveChangesAsync();
            return "Inserted";
        }

        public async Task<string> DeleteNWD(int id)
        {
            NonWorkingDays deletedNWD = await Context.NonWorkingDays.FindAsync(id);
            if (deletedNWD != null)
            {
                Context.NonWorkingDays.Remove(deletedNWD);
                await Context.SaveChangesAsync();
                DeleteMassage = "Removed succesfully";
            }

            else
            {
                DeleteMassage = "Something went wrong , try again";
            }

            return DeleteMassage;

        }


        public async Task<string> DeleteNWD(NonWorkingDays nonWorkingDays)
        {
            return await DeleteNWD(nonWorkingDays.Id);
        }


        public async Task<NonWorkingDays> EditNWD(NonWorkingDays nonWorkingDays)
        {
            var existingNWD = await Context.NonWorkingDays.FindAsync(nonWorkingDays.Id);
            if (existingNWD != null)
            {
                existingNWD.Holiday = nonWorkingDays.Holiday;
                existingNWD.SemesterId = nonWorkingDays.SemesterId; 
                Context.NonWorkingDays.Update(existingNWD);
                await Context.SaveChangesAsync();
            }
            return nonWorkingDays;
        }


        public async Task<NonWorkingDays> GetNWD(int id)
        {
            var nonWorkingDays = await Context.NonWorkingDays.FindAsync(id);
            return nonWorkingDays;
        }

        public IEnumerable<NonWorkingDays> GetAllAvailableNWD()
        {
            return Context.NonWorkingDays.ToList();
        }

        public async void Save()
        {
            await Context.SaveChangesAsync();
        }


        public IEnumerable<NonWorkingDays> FindNonWorkingDaysBySemesterId(int SemesterId)
        {
            return Context.NonWorkingDays.Where(a => a.SemesterId == SemesterId);
        }
    }
}
