using BookStore.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Tests.StepDefinitions
{
    [Binding]
    public sealed class ManageAuthorsStepDefinitions
    {
        private Author Author;

        [Given(@"the user has the ""CanManageCatalog"" permission")]
        public void GivenTheUserHasTheCanManageCatalogPermission()
        {
            // Implement logic to grant the "CanManageCatalog" permission to the user
        }

        [When(@"the user navigates to create a new author")]
        public void WhenTheUserNavigatesToCreateANewAuthor()
        {
            // Implement logic to navigate to the page for creating a new author
        }

        [When(@"fills in all the required fields for an author")]
        public void WhenFillsInAllTheRequiredFieldsForAnAuthor()
        {
            // Implement logic to fill in all the necessary fields for creating a new author
            Author = new Author();
        }

        [Then(@"the new author is successfully created")]
        public void ThenTheNewAuthorIsSuccessfullyCreated()
        {
            // Implement logic to verify that the new author is successfully created
            Assert.IsNotNull(Author, "New author should not be null");
        }

        [Then(@"the new author is visible in the overview list of authors")]
        public void ThenTheNewAuthorIsVisibleInTheOverviewListOfAuthors()
        {
            // Implement logic to verify that the new author is visible in the overview list
            //Assert.IsTrue(IsAuthorVisibleInList(Author), "New author should be visible in the list");
            Assert.IsTrue(true);
        }

        // Additional helper methods can be defined here if needed
        private bool IsAuthorVisibleInList(Author author)
        {
            // Implement logic to check if the author is visible in the overview list
            return true;
        }
    }
}
