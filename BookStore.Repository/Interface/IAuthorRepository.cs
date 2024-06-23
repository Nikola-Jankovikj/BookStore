using BookStore.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Interface
{
    public interface IAuthorRepository
    {
        public Task<IEnumerable<Author>> GetAll();
        public Task<Author> Get(Guid? id);
        public Task<Author> Insert(Author author);
        public Task<Author> Update(Author author);
        public Task<Author> Delete(Author author);
    }
}
