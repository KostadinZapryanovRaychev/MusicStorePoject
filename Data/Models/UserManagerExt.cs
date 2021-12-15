using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Data.Models
{
    public class UserManagerExt : UserManager<ApplicationUser>
    {
        //public async Task<string> GetCurrentUserDetails ()
        //{

        //}
        public UserManagerExt(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

        }

            public async Task<string> GetEmailAsync2(ClaimsPrincipal principal)
            {
                var user = await GetUserAsync(principal);

                var userMail = user.UserPhoto;
                return user.Email;

            }
    }

}


    //return await Task.FromResult("Hi");

    //var user = await GetUserAsync(principal);

    //var userMail = user.Email;


    //return userMail;
    //Thread.Sleep(1000);
    //return await Task.FromResult("Hi");




    //    public string GetEmail2(ClaimsPrincipal principal)
    //    {
            
    //        //return user.Email;
    //        return "Hi";


    //    }
    //}

