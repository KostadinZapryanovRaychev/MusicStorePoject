using Microsoft.EntityFrameworkCore;
using MvcMusicStoreWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject.Data
{
    public class MusicStoreDbContext : DbContext
    {
        public MusicStoreDbContext(DbContextOptions <MusicStoreDbContext> options) : base (options)
        {

        }


        public DbSet <Album> Albums { get; set; }
    }
}
