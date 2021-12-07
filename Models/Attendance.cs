using MvcMusicStoreWebProject.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        //dano tva e pravilniq tip

        [Required]
        // [DataType(DateType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        public string Mode { get; set; }

        [Required]
        public int Course { get; set; }

        [Required]
        public int Groupe { get; set; }

        [Required]

        public string Timeframe { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int Hours { get; set; }

        [Required]
        public string Auditorium { get; set; }

        public string Note { get; set; }

        // traq da go napravq foreign key referirasht zavisimost kum Id v programs

        [ForeignKey("Programs")]
        public string Programs { get; set; }
        // traq da go napravq foreign key referirasht zavisimost kum Id v disciplines / Uj GO napravih nz dali e takaa !!!!!!!!!!!!!!!!!!

        [ForeignKey("Discipline")]
        public string Discipline { get; set; }



        [ForeignKey("Discipline")]
        public int UserId { get; set; }

        public ApplicationUser ApplicationUser  { get; set; }
        //i kak da vkaram kato vunshen kluch sashto taka User ot Identity mnogo vajen moment
    }

    //i kak da vkaram kato vunshen kluch sashto taka User ot Identity mnogo vajen moment
}

