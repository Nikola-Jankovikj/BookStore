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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Author> authors;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
            authors = _context.Set<Author>();
        }

        public async Task<Author> Delete(Author author)
        {
            authors.Remove(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> Get(Guid? id)
        {
            return await authors.Where(a => a.Id == id).Include(a => a.Books).FirstAsync();
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await authors.Include(a => a.Books).ToListAsync();
        }

        public async Task<Author> Insert(Author author)
        {
            authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> Update(Author author)
        {
            authors.Update(author);
            await _context.SaveChangesAsync();
            return author;
        }
    }
}
