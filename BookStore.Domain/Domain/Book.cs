using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Domain
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public int Rating { get; set; }
        public Guid AuthorId { get; set; }
        public virtual Author? Author { get; set; }
        public Guid PublisherId { get; set; }
        public virtual Publisher? Publisher { get; set; }
        public virtual ICollection<ShoppingCartBooks>? BooksInShoppingCart { get; set; }
        public virtual IEnumerable<OrderBooks>? BooksInOrder { get; set; }
    }
}
