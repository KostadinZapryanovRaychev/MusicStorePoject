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
        [Column(TypeName = "date")]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Степен")]
        public string Degree { get; set; }

        [Display(Name = "Форма на обучение")]
        public string Mode { get; set; }

        [Display(Name = "Курс")]
        public int Course { get; set; }

        [Display(Name = "Група")]
        public string Groupe { get; set; }

        [Display(Name = "Времеви диапазон")]

        public string Timeframe { get; set; }

        [Display(Name = "Вид занятие")]
        public string Type { get; set; }

  
        [Display(Name = "Часове")]
        public int Hours { get; set; }

       
        [Display(Name = "Аудитория")]
        public string Auditorium { get; set; }

        [Display(Name = "Забележка")]
        public string Note { get; set; }

        [Display(Name = "Предмет")]
        public string Programs { get; set; }

        [Display(Name = "Специалност")]
        public string Discipline { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser  { get; set; }

        [Display(Name = "Семестър")]
        public int SemesterId { get; set; }

        public Semester Semester { get; set; }

    }

}

