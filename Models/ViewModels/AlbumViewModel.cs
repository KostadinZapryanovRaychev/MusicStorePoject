using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Models.ViewModels
{
    public class AlbumViewModel
    {
        public Album Album { get; set; }

        public List<SelectListItem> AvailableGenres { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem {Text="Reck" , Value="Rock"},
            new SelectListItem {Text="Pop" , Value="Pop"},
            new SelectListItem {Text="Classical" , Value="Classical"},
            new SelectListItem {Text="Jazz" , Value="Jazz"}
        };
    }
}
