﻿using BookStore.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Domain
{
    public class Order : BaseEntity
    {
        public string userId { get; set; }
        public BookStoreUser? Owner { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<OrderBooks> BooksInOrder { get; set; }
    }
}
