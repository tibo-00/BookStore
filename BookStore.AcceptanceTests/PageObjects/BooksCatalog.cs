using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.AcceptanceTests.PageObjects
{
    public class BooksCatalog
    {
        protected IWebDriver _driver;

        public BooksCatalog(IWebDriver driver)
        {
            _driver = driver;
        }

        public List<string> LookForBooks()
        {
            List<string> bookdescriptions = new List<string>();

            var titles = _driver.FindElements(By.ClassName("card-title"));
            foreach (var book in titles)
            {
                bookdescriptions.Add(book.Text);
            }
            var names = _driver.FindElements(By.ClassName("card-subtitle"));
            foreach (var book in names)
            {
                bookdescriptions.Add(book.Text);
            }
            return bookdescriptions;
        }

        public string FindBooksWithGenre(string genre)
        {
            return _driver.FindElements(By.ClassName("card-text"))[1].Text;
        }
    }
}
