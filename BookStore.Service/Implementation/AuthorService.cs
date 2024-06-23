using BookStore.Domain.Domain;
using BookStore.Repository.Interface;
using BookStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> CreateAuthor(Author author)
        {
            return await _authorRepository.Insert(author);
        }

        public async Task<Author> DeleteAuthor(Author author)
        {
            return await _authorRepository.Delete(author);
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _authorRepository.GetAll();
        }

        public async Task<Author> GetAuthorById(Guid id)
        {
            return await _authorRepository.Get(id);
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            return await _authorRepository.Update(author);
        }
    }
}
