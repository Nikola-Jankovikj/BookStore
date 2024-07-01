using BookStore.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interface
{
    public interface IAuthorService
    {
        public Task<IEnumerable<Author>> GetAllAuthors();
        public Task<Author> GetAuthorById(Guid id);
        public Task<Author> CreateAuthor(Author author);
        public Task<Author> UpdateAuthor(Author author);
        public Task<Author> DeleteAuthor(Guid id);
    }
}
