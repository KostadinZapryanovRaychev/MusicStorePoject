using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Models
{
    public class Semester
    {
        public int Id { get; set; }

        public string  Name { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }


    }
}
