using BookStore.Data;
using BookStore.Models.Author;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public ActionResult Overzicht()
        {
            IEnumerable<AuthorListViewModel> model = _authorService.GetAuthorListViewModels();
            return View(model);
        }

        [Authorize(Policy = "CanManageCatalog")]
        public ActionResult Create()
        {
            AuthorViewModel model = new AuthorViewModel();
            return View(model);
        }

        [Authorize(Policy = "CanManageCatalog")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                _authorService.CreateAuthor(model);
                return RedirectToAction("Overzicht");
            }

            return View(model);
        }

        [Authorize(Policy = "CanManageCatalog")]
        public ActionResult Edit(int id)
        {
            Author author = _authorService.GetAuthor(id);
            AuthorViewModel model = new AuthorViewModel();
            model.FirstName = author.FirstName;
            model.LastName = author.LastName;
            model.BirthDate = author.BirthDate;
            return View(model);
        }

        [Authorize(Policy = "CanManageCatalog")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuthorViewModel model, int id)
        {
            _authorService.EditAuthor(model, id);
            return RedirectToAction("Overzicht");
        }

        [Authorize(Policy = "CanManageCatalog")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (_authorService.GetAuthor(id).Books is null)
            {
                _authorService.DeleteAuthor(id);
            }
            return RedirectToAction("Overzicht");
        }
    }
}
