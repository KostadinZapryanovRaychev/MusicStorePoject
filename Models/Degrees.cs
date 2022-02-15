using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Models
{
    public class Degrees
    {
        public int Id { get; set; }

        [Display(Name = "Образователна степен")]
        public string Name { get; set; }

    }
}
