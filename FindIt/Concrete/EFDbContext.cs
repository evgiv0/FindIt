using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FindIt.Models;

namespace FindIt.Concrete
{
    public class EFDbContext: DbContext
    {
        public DbSet<Notice> Notices{ get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}