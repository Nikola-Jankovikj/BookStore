using BookStore.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interface
{
    public interface IBookService
    {
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book> GetBookById(Guid id);
        public Task<Book> CreateBook(Book book);
        public Task UpdateBook(Book book);
        public Task<Book> DeleteBook(Book book);
    }
}
