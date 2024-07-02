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
        public Author Get(Guid? id);
        public Task<Author> Insert(Author author);
        public void Update(Author author);
        public void Delete(Author author);
    }
}
