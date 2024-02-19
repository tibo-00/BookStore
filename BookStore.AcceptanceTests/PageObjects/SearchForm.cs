using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.AcceptanceTests.PageObjects
{
    public class SearchForm
    {
        protected IWebDriver _driver;

        public SearchForm(IWebDriver driver)
        {
            _driver = driver;
        }

        public void WriteSearch(string searchText)
        {
            _driver.FindElement(By.Id("searchTerm")).SendKeys(searchText);
        }

        public void ClickCheckbox(string checkbox)
        {
            _driver.FindElement(By.Id(checkbox)).Click();
        }

        public void SelectGenre(string genre) 
        {
            _driver.FindElement(By.Id("BookGenre")).FindElement(By.XPath($"//option[text()='{genre}']")).Click();
        }

        public void SubmitSearch()
        {
            _driver.FindElement(By.ClassName("btn-primary")).Click();
        }
    }
}
