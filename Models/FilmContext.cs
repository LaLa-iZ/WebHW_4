using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebHW_4.Models
{
    public class FilmContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Producer> Producers { get; set; }

        public FilmContext(DbContextOptions<FilmContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
