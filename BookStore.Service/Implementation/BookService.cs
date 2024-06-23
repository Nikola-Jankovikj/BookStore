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
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> CreateBook(Book book)
        {
            return await _bookRepository.Insert(book);
        }

        public async Task<Book> DeleteBook(Book book)
        {
            return await _bookRepository.Delete(book);
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _bookRepository.GetAll();
        }

        public async Task<Book> GetBookById(Guid id)
        {
            return await _bookRepository.Get(id);
        }

        public async Task UpdateBook(Book book)
        {
            await _bookRepository.Update(book);
        }
    }
}
