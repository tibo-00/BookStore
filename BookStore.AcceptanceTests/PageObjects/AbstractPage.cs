using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.AcceptanceTests.PageObjects
{
    public class AbstractPage
    {
        protected IWebDriver driver;

        public AbstractPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public AbstractPage NavigateToHomePage()
        {
            driver.Navigate().GoToUrl("https://localhost:7110/");
            return this;
        }

        public AbstractPage NavigateToCatalogPage()
        {
            driver.Navigate().GoToUrl("https://localhost:7110/Book/Catalog");
            return this;
        }

        public String GetCurrentUrl()
        {
            return driver.Url;
        }

        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
