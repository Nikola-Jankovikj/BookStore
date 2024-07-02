using BookStore.Domain.Domain;
using BookStore.Service.Implementation;
using BookStore.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Web.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAuthors();
            return View(authors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Description, ImageUrl")] Author author)
        {
            if (ModelState.IsValid)
            {
                author.Id = Guid.NewGuid();
                await _authorService.CreateAuthor(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var author = await _authorService.GetAuthorById(id);
            return View(author);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var author = await _authorService.GetAuthorById(id);
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Name,Description, ImageUrl")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _authorService.UpdateAuthor(author);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var author = await _authorService.GetAuthorById(id);
            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _authorService.DeleteAuthor(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
