using BookStore.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.AcceptanceTests.StepDefinitions
{
    [Binding]
    public sealed class CalculateDiscountStepDefinitions
    {
        private bool isEmployee;
        private float subtotal;
        private int amountOfBooks;
        private int discountPercentage;
        private float calculatedTotal;

        [Given(@"the user is a customer")]
        public void GivenTheUserIsACustomer()
        {
            isEmployee = false;
        }

        [Given(@"the user is an employee")]
        public void GivenTheUserIsAnEmployee()
        {
            isEmployee = true;
        }

        [When(@"the user checks out with a subtotal of (.*) euro and (.*) books?")]
        public void WhenTheUserChecksOutWithASubtotalOfEuroAndBooks(float subtotal, int amountOfBooks)
        {
            this.subtotal = subtotal;
            this.amountOfBooks = amountOfBooks;
        }

        [Then(@"the discount should be (.*)%, resulting in a total of (.*) euro")]
        public void ThenTheDiscountShouldBeResultingInATotalOfEuro(int expectedDiscount, float expectedTotal)
        {
            discountPercentage = DiscountService.GetDiscountPercentage(isEmployee, subtotal, amountOfBooks);

            Assert.AreEqual(expectedDiscount, discountPercentage, "Discount percentage mismatch");

            calculatedTotal = DiscountService.CalculateTotal(discountPercentage, subtotal);

            Assert.AreEqual(expectedTotal, calculatedTotal, "Total amount mismatch");
        }
    }
}
