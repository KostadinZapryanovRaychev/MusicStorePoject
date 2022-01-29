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
    public class DegreeRepository : IDegreeRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private MusicStoreDbContext Context { get; }
        public IConfiguration Config { get; }
        private string DeleteMassage { get; set; }
        private string ExceptionMessage { get; set; }
        public DegreeRepository(MusicStoreDbContext context, IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            Context = context;
            Config = config;
            ExceptionMessage = config.GetSection("RepositoryExceptionMassage").Value;
        }
        public async Task<string> AddDegrees(Degrees degrees)
        {
            Context.Degrees.Add(degrees);
            await Context.SaveChangesAsync();
            return "Inserted";
        }

        public async Task<string> DeleteDegreesById(int id)
        {
            Degrees deletedDegrees = await Context.Degrees.FindAsync(id);
            if (deletedDegrees != null)
            {
                Context.Degrees.Remove(deletedDegrees);
                await Context.SaveChangesAsync();
                DeleteMassage = "Removed succesfully";
            }

            else
            {
                DeleteMassage = "Something went wrong , try again";
            }

            return DeleteMassage;

        }


        public async Task<string> DeleteDegrees(Degrees degrees)
        {
            return await DeleteDegreesById(degrees.Id);
        }


        public async Task<Degrees> EditDegrees(Degrees degrees)
        {
            var existingDegree = await Context.Degrees.FindAsync(degrees.Id);
            if (existingDegree != null)
            {
                existingDegree.Name = degrees.Name;

                Context.Degrees.Update(existingDegree);
                await Context.SaveChangesAsync();
            }
            return degrees;
        }


        public async Task<Degrees> GetDegrees(int id)
        {
            var degrees = await Context.Degrees.FindAsync(id);
            return degrees;
        }

        public IEnumerable<Degrees> GetAllAvailableDegrees()
        {
            return Context.Degrees.ToList();
        }

        public async void Save()
        {
            await Context.SaveChangesAsync();
        }
    }
}
