using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Data
{
    public interface IRepository
    {
         Task <Album> GetAlbumById(int Id);

        IEnumerable<Album> GetAllAblums();

        Task <Album> UpdateAlbum(Album album);

        string DeleteAlbum(int Id);

        // toq izobshto ne go i razbiram
        string DeleteAlbum(Album album);

        string InsertNewAlbum(Album album);

        Task <int >Save();
    }
}
