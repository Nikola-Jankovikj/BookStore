using BookStore.Domain.Domain;
using BookStore.Domain.DTO;
using BookStore.Repository.Implementation;
using BookStore.Repository.Interface;
using BookStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRepository<OrderBooks> _booksInOrderRepository;
        private readonly IUserRepository _userRepository;
        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IRepository<OrderBooks> booksInOrderRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _booksInOrderRepository = booksInOrderRepository;
        }
        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        public List<Order> GetAllOrdersByUser(string userId)
        {
            return _orderRepository.GetAllOrdersByUser(userId);
        }

        public OrderDto GetDetailsForOrder(Guid id)
        {
            var order = _orderRepository.GetDetailsForOrder(id);
            var allBooks = order?.BooksInOrder?.ToList();

            var totalPrice = allBooks.Select(x => (x.Book.Price * x.Quantity)).Sum();

            OrderDto dto = new OrderDto
            {
                Id = order.Id.ToString(),
                OwnerEmail = order.Owner.Email,
                Books = allBooks,
                TotalPrice = totalPrice
            };

            return dto;
        }
    }
}
