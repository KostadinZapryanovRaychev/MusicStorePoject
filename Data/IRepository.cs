using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcMusicStoreWebProject.Data
{
    public interface IRepository
    {
         Task <Album> GetAlbumById(int Id);

        IEnumerable<Album> GetAllAblums();

        Task <Album> UpdateAlbum(Album album);

        Task<string> DeleteAlbum(int Id);

        // toq izobshto ne go i razbiram
        Task <string> DeleteAlbum(Album album);

        Task <string> InsertNewAlbum(Album album);

        bool IsDuplicateTitle(string Title);
        Task <int >Save();
    }
}
