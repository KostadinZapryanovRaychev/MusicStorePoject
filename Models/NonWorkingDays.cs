using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Models
{
    public class NonWorkingDays
    {
        public int Id{ get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Почивен ден")]
        public DateTime Holiday { get; set; }


    }
}
