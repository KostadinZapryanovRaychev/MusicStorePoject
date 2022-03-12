using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MvcMusicStoreWebProject.Data.Models;
using MvcMusicStoreWebProject.Models;
using MvcMusicStoreWebProject.Data;

namespace MvcMusicStoreWebProject.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private static IAPtRRepository _repo;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender , IAPtRRepository repo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _repo = repo;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Имейл")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Потвърджаване на парола")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

           
            [Display(Name = "Име, Презиме, Фамилия")]
            public string OfficialName { get; set; }

            [Display(Name = "Академична длъжност и степен")]
            public string Description { get; set; }
        }


        //TODO // da idva ot bazata danni tva i da razdelq akademichnata stepen i trite imena na 2 chasti

        public List<string> AllowedNames { get; set; } = new List<string>()
        {
            new string ("Георги Иванов Георгиев"),
            new string ("Цветан Иванов Георгиев"),
            new string ("Михаил Иванов Георгиев"),
            new string ("Трендафил Иванов Георгиев"),
            new string ("Божидар Иванов Георгиев"),
            new string ("Манол Иванов Георгиев"),
            new string ("Костадин Иванов Георгиев"),
        };


        //public IList<AllowedPersonsToRegister> ANames ()
        //{
        //    var result = Context.NonWorkingDays.ToList();

        //    // правим нов лист от тип Datetime
        //    var holidays = new List<DateTime>();

        //    // попълваме листа с данните от таблицата НонУъркингДейс
        //    foreach (var holiday in result)
        //    {

        //        holidays.Add(holiday.Holiday);
        //    };
        //}

        //public IList<Discipline> GetDisciplinesByProgramId(int DegreesId)
        //{
        //    var disciplinelist = from discipline in Context.Disciplines where discipline.DegreesId == DegreesId select discipline;
        //    var disciplineNames = disciplinelist.ToList<Discipline>();
        //    var onlyNames = new List<string>();

        //    foreach (var onlyName in disciplineNames)
        //    {

        //        onlyNames.Add(onlyName.Name);
        //    };

        //    return disciplineNames;
        //}

        



        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {




                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email , OfficialName=Input.OfficialName , Description=Input.Description };

                var allowdNames = _repo.ANames();

                if (allowdNames.Contains(user.OfficialName)) {
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = "", returnUrl = returnUrl },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        }
                        else
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
