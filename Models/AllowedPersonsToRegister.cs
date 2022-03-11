using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Models
{
    public class AllowedPersonsToRegister
    {
        public int Id { get; set; }

        [Display(Name = "Три имена")]
        public string Name { get; set; }
    }
}
