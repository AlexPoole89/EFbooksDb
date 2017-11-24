using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz3Books
{
    class LibraryDbContext : DbContext 
    {
        public LibraryDbContext() : base()
        {
        }

        public virtual DbSet<Book> Books { get; set; }
    }
}
