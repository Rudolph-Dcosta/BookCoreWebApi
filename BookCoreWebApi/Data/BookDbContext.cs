using BookCoreWebApi.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookCoreWebApi.Data
{
    public class BookDbContext:DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options):base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
    }
}
