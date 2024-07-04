using BookStore.Domain.Domain;
using BookStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.BooksInOrder)
                .Include(z => z.Owner)
                .Include("BooksInOrder.Book")
                .Include("BooksInOrder.Book.Author")
                .Include("BooksInOrder.Book.Publisher")
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public List<Order> GetAllOrdersByUser(string userId)
        {
            return entities
                .Where(z => z.userId == userId)
                .Include(z => z.BooksInOrder)
                .Include(z => z.Owner)
                .Include("BooksInOrder.Book")
                .Include("BooksInOrder.Book.Author")
                .Include("BooksInOrder.Book.Publisher")
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public Order GetDetailsForOrder(Guid? id)
        {
            return entities
                .Include(z => z.BooksInOrder)
                .Include(z => z.Owner)
                .Include("BooksInOrder.Book")
                .Include("BooksInOrder.Book.Author")
                .Include("BooksInOrder.Book.Publisher")
                .SingleOrDefaultAsync(z => z.Id == id).Result;
        }
    }
}
