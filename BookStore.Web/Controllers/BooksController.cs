using BookStore.Domain.Domain;
using BookStore.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookStore.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IPublisherService _publisherService;
        private readonly IAuthorService _authorService;
        private readonly IShoppingCartService _shoppingCartService;

        public BooksController(IBookService bookService, IShoppingCartService shoppingCartService, IPublisherService publisherService, IAuthorService authorService)
        {
            _bookService = bookService;
            _shoppingCartService = shoppingCartService;
            _publisherService = publisherService;
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooks();
            return View(books);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.PublisherId = new SelectList(_publisherService.GetAllPublishers(), "Id", "Name");
            ViewBag.AuthorId = new SelectList(await _authorService.GetAllAuthors(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ImageUrl,Price,Rating,Genre,AuthorId, PublisherId")] Book book)
        {
            /*book.Id = Guid.NewGuid();*/
            if (ModelState.IsValid)
            {
                book.Id = Guid.NewGuid();
                await _bookService.CreateBook(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var book = await _bookService.GetBookById(id);
            return View(book);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var book = await _bookService.GetBookById(id);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Title,Description,ImageUrl,Price,Rating,Genre,AuthorId, PublisherId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookService.UpdateBook(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var book = await _bookService.GetBookById(id);
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _bookService.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddToCart(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }

            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBookById(id);

            ShoppingCartBooks sb = new ShoppingCartBooks();

            if (book != null)
            {
                sb.BookId = book.Id;
            }

            return View(sb);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCartConfirmed(ShoppingCartBooks model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.AddToShoppingConfirmed(model, userId);

            var books = await _bookService.GetAllBooks();

            return View("Index", books);
        }
    }
}
