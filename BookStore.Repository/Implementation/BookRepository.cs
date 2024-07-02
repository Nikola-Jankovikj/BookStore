using BookStore.Domain.Domain;
using BookStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Implementation
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Book> books;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
            books = context.Set<Book>();
        }

        public void Delete(Book book)
        {
            books.Remove(book);
            _context.SaveChanges();
        }

        public Book Get(Guid? id)
        {
            return books
			   .Include(z => z.Author)
			   .Include(z => z.Publisher)
			   .SingleOrDefault(s => s.Id == id);
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .ToListAsync();
        }

        public async Task<Book> Insert(Book book)
        {
            books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public void Update(Book book)
        {
            books.Update(book);
            _context.SaveChanges();
        }
    }
}
