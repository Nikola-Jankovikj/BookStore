﻿using BookStore.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Interface
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAll();
        public Task<Book> Get(Guid? id);
        public Task<Book> Insert(Book book);
        public void Update(Book book);
        public Task<Book> Delete(Book book);
    }
}
