using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Isbn13Validation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string isbn = Convert.ToString(value);

            if (string.IsNullOrEmpty(isbn) || isbn.Length != 13)
            {
                return new ValidationResult("ISBN-13 must be 13 digits!");
            }

            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int digit = isbn[i] - '0';

                if (i % 2 == 0)
                {
                    sum += digit;
                }
                else
                {
                    sum += 3 * digit;
                }
            }

            int checkDigit = 10 - (sum % 10);
            if (checkDigit == 10)
            {
                checkDigit = 0;
            }

            if (isbn[12] - '0' != checkDigit)
            {
                return new ValidationResult("Invalid ISBN-13 check digit.");
            }

            return ValidationResult.Success;
        }
    }
}
