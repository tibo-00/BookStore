using BookStore.AcceptanceTests.PageObjects;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BookStore.Services;
using BookStore.Data;
using BookStore.Models.Author;
using BookStore.Models.Book;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Intrinsics.X86;

namespace BookStore.AcceptanceTests.StepDefinitions
{
    [Binding]
    public sealed class SearchbarUIStepDefinitions
    {
        private IWebDriver _driver;
        private AbstractPage _catalogPage;
        private SearchForm _searchForm;
        private BooksCatalog _booksCatalog;
        private String _searchString;

        [AfterScenario]
        public void TearDown()
        {
            if(_catalogPage!= null)
            {
                _catalogPage.CloseBrowser();
            }
        }


        [Given(@"I am using the web app")]
        public void GivenIAmUsingTheApp()
        {
            _driver = new ChromeDriver();
            _catalogPage = new AbstractPage(_driver);
            _searchForm = new SearchForm(_driver);
            _booksCatalog = new BooksCatalog(_driver);
        }

        [Given(@"I am on the book catalog page")]
        public void GivenIAmOnTheBookCatalogPage()
        {
            _catalogPage.NavigateToCatalogPage();
        }

        [When(@"I search for a book with the (.*): (.*)")]
        public void WhenISearchForABookWithTheSearchText(string useless, string searchText)
        {
            _searchString = searchText;
            _searchForm.WriteSearch(searchText);
        }

        [When(@"I check the (.*) checkbox")]
        public void WhenIHaveCheckedTheCheckbox(string checkbox)
        {
            _searchForm.ClickCheckbox(checkbox);
        }

        [When(@"I select (.*) as Genre")]
        public void GivenISelectActionAsGenre(string genre)
        {
            _searchForm.SelectGenre(genre);
        }

        [When(@"click submit")]
        public void WhenClickSubmit()
        {
            _searchForm.SubmitSearch();
        }

        [Then(@"I should see the book in the catalog list")]
        public void ThenIShouldSeeTheDetailsOfTheBookInTheCatalog()
        {
            List<string> bookdescriptions = _booksCatalog.LookForBooks();
            foreach (string bookdesc in bookdescriptions)
            {
                if (bookdesc.Contains(_searchString))
                {
                    Assert.True(true);
                    return;
                }
            }

            Assert.True(false, "The book is not in the catalog when it should be");
        }

        [Then(@"I should not see the book in the catalog list")]
        public void ThenIShouldNotSeeTheDetailsOfTheBookInTheCatalog()
        {
            List<string> bookdescriptions = _booksCatalog.LookForBooks();
            foreach (string bookdesc in bookdescriptions)
            {
                if (bookdesc.Contains(_searchString))
                {
                    Assert.False(true, "The book is in the catalog when it should not be");
                    return;
                }
            }

            Assert.False(false);
        }


        [Then(@"I should see books in the catalog list with category (.*)")]
        public void ThenIShouldSeeBooksInTheCatalogListWithCategoryAction(string genre)
        {
            string genreElement = _booksCatalog.FindBooksWithGenre(genre);

            if (genreElement.Contains(genre))
            {
                Assert.True(true);
                return;
            }
            Assert.True(false, $"No book found with genre {genre}");

        }

        [Then(@"I should not see any books in the catalog list")]
        public void ThenIShouldNotSeeAnyBooksInTheCatalog()
        {
            List<string> bookdescriptions = _booksCatalog.LookForBooks();
           
            Assert.IsEmpty(bookdescriptions, "Books are found when their shouldn't be");
        }

        [Then(@"I should see books in the catalog list")]
        public void ThenISeeBooksInTheCatalog()
        {
            List<string> bookdescriptions = _booksCatalog.LookForBooks();

            Assert.IsNotEmpty(bookdescriptions, "Books are not found their should be");
        }
    }
}
