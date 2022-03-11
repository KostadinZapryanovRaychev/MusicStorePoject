using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Data
{
    public interface IAPtRRepository
    {
        void Save();
        IEnumerable<AllowedPersonsToRegister> GetAllAvailableAPtoReg();

        Task<AllowedPersonsToRegister> GetAPtoReg(int id);
        Task<AllowedPersonsToRegister> EditAPtoReg(AllowedPersonsToRegister allowedPersonsToRegister);

        Task<string> DeleteAPtoReg(AllowedPersonsToRegister allowedPersonsToRegister);

        Task<string> DeleteAPtoReg(int id);

        Task<string> AddAPtoReg(AllowedPersonsToRegister allowedPersonsToRegister);

        
    }
}
