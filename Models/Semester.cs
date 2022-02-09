using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Models
{
    public class Semester
    {
        public int Id { get; set; }

        [Display(Name = "Име")]

        public string  Name { get; set; }

        [Display(Name = "Начална дата")]
        [Column(TypeName = "date")]
        public DateTime startDate { get; set; }

        [Display(Name = "Крайна дата")]
        [Column(TypeName = "date")]
        public DateTime endDate { get; set; }


    }
}
