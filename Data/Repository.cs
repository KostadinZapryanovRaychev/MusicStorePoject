using Microsoft.Extensions.Configuration;
using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcMusicStoreWebProject.Data
{
    public class Repository : IRepository
    {
        public Repository(MusicStoreDbContext context , IConfiguration config)
        {
            Context = context;
            Config = config;
            ExceptionMessage = config.GetSection("RepositoryExceptionMassage").Value;
        }

        private MusicStoreDbContext Context { get; }
        public IConfiguration Config { get; }
        private string DeleteMassage { get; set; }
        private string ExceptionMessage { get; set; }

        public async Task <string> DeleteAlbum(int Id)
        {
            Album deleteAlbum =  Context.Albums.Find(Id);
            int deletedItems = 0;

            try
            {
                if (deleteAlbum != null)
                {
                    Context.Albums.Remove(deleteAlbum);
                    deletedItems =await  Context.SaveChangesAsync();
                    

                }
                if (deletedItems > 0)
                    return DeleteMassage = $"Deletion of Album with Id = {deleteAlbum.Id}  has been succesfull.";

                return DeleteMassage = $"Deletion of Album with Id = {deleteAlbum.Id}  failed.";

            } catch (Exception exception)
            {
                return ExceptionMessage;
            }



         }

        public async Task<string> DeleteAlbum(Album album)
        {
            return await DeleteAlbum(album.Id);
        }

        public async Task <Album> GetAlbumById(int Id)
        {
            return await  Context.Albums.FindAsync(Id);
        }

        public IEnumerable<Album> GetAllAblums()
        {
            // tva v detaili ne go razbiram
            return Context.Albums.AsEnumerable();
        }


        // receiveme Album kato argument shtot vse pak shte promenqme cql obekt ir aznite mu svoistva
        public async Task <Album> UpdateAlbum(Album album)
        {
            // kuv mu e filma kuv e toq Context traq nqkoi da mi obqsni
            Context.Albums.Update(album);
            var intChanges =await Context.SaveChangesAsync();

            if (intChanges > 0)
                return album;
            else
                return null;
        }

        public bool IsDuplicateTitle (string title)
        {
            // the Context represents the whole database
            // tva ako nqkoi mi obqsni kak raboti napravo sha kandidatstvam v NASA i direktno s tva sha im sa hvalq
            var duplicateAlbum = Context.Albums.FirstOrDefault((album) => album.Title == title);

            if (duplicateAlbum != null)
                return true;

            return false;
       
        }

        public async Task <string>InsertNewAlbum(Album album)
        {
            Context.Albums.Add(album);
            await Context.SaveChangesAsync();
            return "Inserted";
        }

        public async Task<int> Save()
        {
            var result = await Context.SaveChangesAsync();
            return result;
        }
    }
}
