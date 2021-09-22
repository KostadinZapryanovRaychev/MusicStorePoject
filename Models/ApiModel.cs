using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Models
{
    // s F12 kato cuknem vurhu neshtu i otivame vuv nego
    public class ApiModel
    {
        public int UserId { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsDuplicated { get; set; } = false;
    }
}
