using BookStore.Domain.Domain;
using BookStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

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

        public void Delete(Author author)
        {
            authors.Remove(author);
            _context.SaveChanges();
        }

        public Author Get(Guid? id)
        {
            return authors
               .Include(z => z.Books)
               .SingleOrDefault(s => s.Id == id);
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

        public void Update(Author author)
        {
            authors.Update(author);
            _context.SaveChanges();
        }
    }
}
