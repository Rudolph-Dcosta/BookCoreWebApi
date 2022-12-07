using AutoMapper;
using BookCoreWebApi.Data;
using BookCoreWebApi.Models.Domain;
using BookCoreWebApi.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookCoreWebApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _bookDbContext;
        private readonly IMapper _mapper;
      
        public BookRepository(BookDbContext bookDbContext,IMapper mapper)
        {
            _bookDbContext=bookDbContext;
            _mapper=mapper;
        }
        public async Task<Book> AddBookAsync(Book book)
        {
            await _bookDbContext.Books.AddAsync(book);
            await _bookDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book> DeleteBookAsync(int id)
        {
            var book = await _bookDbContext.Books.FirstOrDefaultAsync(X => X.Id == id);
            if(book == null)
            {
                return null;
            }
             _bookDbContext.Books.Remove(book);
            await _bookDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var allBook = await _bookDbContext.Books.ToListAsync();
            return allBook;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _bookDbContext.Books.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Book> UpdateBookAsync(int id, Book book)
        {
            var existingBook  = await _bookDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if(existingBook == null)
            {
                return null;
            }
            existingBook.Name=book.Name;    
            existingBook.DisplayOrder=book.DisplayOrder;
            await _bookDbContext.SaveChangesAsync();
            return existingBook;

        }
    }
}
