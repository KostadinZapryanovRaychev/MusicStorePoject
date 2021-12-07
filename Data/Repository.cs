using Microsoft.Extensions.Configuration;
using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMusicStoreWebProject.Models.ViewModels;

namespace MvcMusicStoreWebProject.Data
{
    //public class Repository : IRepository
    //{
    //    public Repository(MusicStoreDbContext context , IConfiguration config)
    //    {
    //        Context = context;
    //        Config = config;

    //        // taq tupotiq sled kato injektnetm IConfiguration i mu dadem nqkvo ime primero confir , chrez config.GetSection dostupvame stringove vuv (daje sectioni) vuv appjson
    //        ExceptionMessage = config.GetSection("RepositoryExceptionMassage").Value;
    //    }

    //    private MusicStoreDbContext Context { get; }
    //    public IConfiguration Config { get; }
    //    private string DeleteMassage { get; set; }
    //    private string ExceptionMessage { get; set; }

    //    public async Task <string> DeleteAlbum(int Id)
    //    {
    //        Album deleteAlbum =  Context.Albums.Find(Id);
    //        int deletedItems = 0;

    //        try
    //        {
    //            if (deleteAlbum != null)
    //            {
    //                Context.Albums.Remove(deleteAlbum);
    //                deletedItems =await  Context.SaveChangesAsync();


    //            }
    //            if (deletedItems > 0)
    //                return DeleteMassage = $"Deletion of Album with Id = {deleteAlbum.Id}  has been succesfull.";

    //            return DeleteMassage = $"Deletion of Album with Id = {deleteAlbum.Id}  failed.";

    //        } catch (Exception exception)
    //        {
    //            return ExceptionMessage;
    //        }



    //     }

    //    // shto dva pati e razpisana delete funkciqta
    //    public async Task<string> DeleteAlbum(Album album)
    //    {
    //        return await DeleteAlbum(album.Id);
    //    }

    //    public async Task <Album> GetAlbumById(int Id)
    //    {
    //        return await  Context.Albums.FindAsync(Id);
    //    }

    //    public IEnumerable<Album> GetAllAblums()
    //    {
    //        // tva v detaili ne go razbiram
    //        return Context.Albums.AsEnumerable();
    //    }


    //    // receiveme Album kato argument shtot vse pak shte promenqme cql obekt ir aznite mu svoistva
    //    public async Task <Album> UpdateAlbum(Album album)
    //    {
    //        // kuv mu e filma kuv e toq Context traq nqkoi da mi obqsni
    //        Context.Albums.Update(album);
    //        var intChanges =await Context.SaveChangesAsync();

    //        if (intChanges > 0)
    //            return album;
    //        else
    //            return null;
    //    }

    //    public bool IsDuplicateTitle (string title)
    //    {
    //        // the Context represents the whole database
    //        // tva ako nqkoi mi obqsni kak raboti napravo sha kandidatstvam v NASA i direktno s tva sha im sa hvalq
    //        var duplicateAlbum = Context.Albums.FirstOrDefault((album) => album.Title == title);

    //        if (duplicateAlbum != null)
    //            return true;

    //        return false;

    //    }

    //    public async Task <string>InsertNewAlbum(Album album)
    //    {
    //        Context.Albums.Add(album);
    //        await Context.SaveChangesAsync();
    //        return "Inserted";
    //    }

    //    public async Task<int> Save()
    //    {
    //        var result = await Context.SaveChangesAsync();
    //        return result;
    //    }
    //}

    public class Repository : IRepository
    {
        private MusicStoreDbContext Context { get; }
        public IConfiguration Config { get; }
        private string DeleteMassage { get; set; }
        private string ExceptionMessage { get; set; }
        public Repository(MusicStoreDbContext context, IConfiguration config)
        {
            Context = context;
            Config = config;
            ExceptionMessage = config.GetSection("RepositoryExceptionMassage").Value;
        }
        public async Task<string> AddAttendance(Attendance attendance)
        {
            Context.Attendances.Add(attendance);
            await Context.SaveChangesAsync();
            return "Inserted";
        }


        // tuk kva e razlikata dali shte go narpavq taka ili s ID nemoga da razbera
        public async Task<string> DeleteAttendance(int id)
        {
            Attendance deletedAttendance = await Context.Attendances.FindAsync(id);
            if (deletedAttendance != null)
            {
                Context.Attendances.Remove(deletedAttendance);
                await Context.SaveChangesAsync();
                DeleteMassage = "Removed succesfully";
            }

            else
            {
                DeleteMassage = "Something went wrong , try again";
            }

            return DeleteMassage;

        }

        public async Task<string> DeleteAttendance(Attendance attendance)
        {
            return await DeleteAttendance(attendance.Id);
        }

        public async Task<Attendance> EditAttendance(Attendance attendance)
        {
            var existingAttend = await Context.Attendances.FindAsync(attendance.Id);

            if (existingAttend != null)
            {
                existingAttend.Hours = attendance.Hours;
                existingAttend.Groupe = attendance.Groupe;
                existingAttend.Type = attendance.Type;
                existingAttend.Mode = attendance.Mode;
                existingAttend.Timeframe = attendance.Timeframe;
                existingAttend.Programs = attendance.Programs;
                existingAttend.Auditorium = attendance.Auditorium;
                existingAttend.Degree = attendance.Degree;
                existingAttend.Note = attendance.Note;
                existingAttend.Course = attendance.Course;

                Context.Attendances.Update(existingAttend);
                await Context.SaveChangesAsync();
            }
            return attendance;

        }

        public async Task<Attendance> GetAttendance(int id)
        {
            // ako id to vkarano kato parametar v taq funkciq e ravno na Id na Attendance vurni mi toq attendance !
            // return Context.Attendances.SingleOrDefault(x => x.Id == id);

            // durgiq variant e 

            var attend = await Context.Attendances.FindAsync(id);
            return attend;
        }


        // ei tva trqbva da listne vsichki attendances
        public IEnumerable<Attendance> GetAttendances()
        {
            return Context.Attendances.ToList();
        }

        public async Task<List<ProgramsViewModel>> GetPrograms()
        {
            return await Context.Programs.Select(x => new ProgramsViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        public async Task<List<DisciplineViewModel>> GetDiscipline()
        {
            return await Context.Disciplines.Select(x => new DisciplineViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }
    }
}
