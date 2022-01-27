using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Models
{
    public class NonWorkingDays
    {
        public int Id{ get; set; }

        [Column(TypeName = "date")]
        public DateTime Holiday { get; set; }


        //TODO: to remove SemesterId from this model and to re run migrations 
        public int SemesterId { get; set; }

        public Semester Semester { get; set; }


    }
}
