using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Models.ViewModels
{
    public class BMDisciplines
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DegreesId { get; set; }
        public Degrees Degrees { get; set; }
    }
}
