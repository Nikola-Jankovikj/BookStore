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
        public Author GetAuthorById(Guid id);
        public Task<Author> CreateAuthor(Author author);
        public void UpdateAuthor(Author author);
        public void DeleteAuthor(Guid id);
    }
}
