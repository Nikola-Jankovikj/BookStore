using BookStore.Domain.Domain;
using BookStore.Domain.DTO;
using BookStore.Repository.Interface;
using BookStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<ShoppingCartBooks> _booksInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderBooks> _booksInOrderRepository;

        public ShoppingCartService(IRepository<OrderBooks> booksInOrderRepository,
            IRepository<Order> _orderRepository,
            IUserRepository userRepository,
            IRepository<ShoppingCart> shoppingCartRepository,
            IRepository<ShoppingCartBooks> booksInShoppingCartRepository)
        {
            this._booksInOrderRepository = booksInOrderRepository;
            this._orderRepository = _orderRepository;
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _booksInShoppingCartRepository = booksInShoppingCartRepository;
        }

        public bool AddToShoppingConfirmed(ShoppingCartBooks model, string userId)
        {

            var loggedInUser = _userRepository.Get(userId);

            var userShoppingCart = loggedInUser.ShoppingCart;

            if (userShoppingCart.BooksInShoppingCart == null)
                userShoppingCart.BooksInShoppingCart = new List<ShoppingCartBooks>(); ;

            userShoppingCart.BooksInShoppingCart.Add(model);
            _shoppingCartRepository.Update(userShoppingCart);
            return true;
        }

        public bool deleteBookFromShoppingCart(string userId, Guid bookId)
        {
            if (bookId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userShoppingCart = loggedInUser.ShoppingCart;
                var book = userShoppingCart.BooksInShoppingCart.Where(x => x.BookId == bookId).FirstOrDefault();

                userShoppingCart.BooksInShoppingCart.Remove(book);

                _shoppingCartRepository.Update(userShoppingCart);
                return true;
            }
            return false;

        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            var userShoppingCart = loggedInUser?.ShoppingCart;
            var allBooks = userShoppingCart?.BooksInShoppingCart?.ToList();

            var totalPrice = allBooks.Select(x => (x.Book.Price * x.Quantity)).Sum();

            ShoppingCartDto dto = new ShoppingCartDto
            {
                Books = allBooks,
                TotalPrice = totalPrice
            };
            return dto;
        }

        public bool order(string userId)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userShoppingCart = loggedInUser.ShoppingCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    userId = userId,
                    Owner = loggedInUser
                };

                _orderRepository.Insert(order);

                List<OrderBooks> booksInOrder = new List<OrderBooks>();

                var lista = userShoppingCart.BooksInShoppingCart.Select(
                    x => new OrderBooks
                    {
                        Id = Guid.NewGuid(),
                        BookId = x.Book.Id,
                        Book = x.Book,
                        OrderId = order.Id,
                        Order = order,
                        Quantity = x.Quantity
                    }
                    ).ToList();

                booksInOrder.AddRange(lista);

                foreach (var book in booksInOrder)
                {
                    _booksInOrderRepository.Insert(book);
                }

                loggedInUser.ShoppingCart.BooksInShoppingCart.Clear();
                _userRepository.Update(loggedInUser);

                return true;
            }
            return false;
        }

    }
}
