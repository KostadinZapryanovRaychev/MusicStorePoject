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
    public class APtRRepository : IAPtRRepository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private MusicStoreDbContext Context { get; }
        public IConfiguration Config { get; }
        private string DeleteMassage { get; set; }
        private string ExceptionMessage { get; set; }

        public APtRRepository(MusicStoreDbContext context, IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            Context = context;
            Config = config;
            ExceptionMessage = config.GetSection("RepositoryExceptionMassage").Value;
        }

        public async Task<string> AddAPtoReg(AllowedPersonsToRegister allowedPersonsToRegister)
        {
            Context.AllowedPersonsToRegisters.Add(allowedPersonsToRegister);
            await Context.SaveChangesAsync();
            return "Inserted";
        }

        public async Task<string> DeleteAPtoReg(int id)
        {
            AllowedPersonsToRegister deleteAPtoReg = await Context.AllowedPersonsToRegisters.FindAsync(id);
            if (deleteAPtoReg != null)
            {
                Context.AllowedPersonsToRegisters.Remove(deleteAPtoReg);
                await Context.SaveChangesAsync();
                DeleteMassage = "Removed succesfully";
            }

            else
            {
                DeleteMassage = "Something went wrong , try again";
            }

            return DeleteMassage;

        }


        public async Task<string> DeleteAPtoReg(AllowedPersonsToRegister allowedPersonsToRegister)
        {
            return await DeleteAPtoReg(allowedPersonsToRegister.Id);
        }


        public async Task<AllowedPersonsToRegister> EditAPtoReg(AllowedPersonsToRegister allowedPersonsToRegister)
        {
            var existingAPtoReg = await Context.AllowedPersonsToRegisters.FindAsync(allowedPersonsToRegister.Id);
            if (existingAPtoReg != null)
            {
                existingAPtoReg.Name = allowedPersonsToRegister.Name;
                Context.AllowedPersonsToRegisters.Update(existingAPtoReg);
                await Context.SaveChangesAsync();
            }
            return allowedPersonsToRegister;
        }


        public async Task<AllowedPersonsToRegister> GetAPtoReg(int id)
        {
            var allowedPersonToReg = await Context.AllowedPersonsToRegisters.FindAsync(id);
            return allowedPersonToReg;
        }

        public IEnumerable<AllowedPersonsToRegister> GetAllAvailableAPtoReg()
        {
            return Context.AllowedPersonsToRegisters.ToList();
        }

        public async void Save()
        {
            await Context.SaveChangesAsync();
        }

    }
}
