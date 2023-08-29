using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MuranoTest.Models;

namespace MuranoTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }
        public DbSet<UrlObject> Urls { get; set; }
        public DbSet<Word> Words { get; set; }
    }
}