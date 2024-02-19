namespace BookStore.Services
{
    public static class DiscountService
    {
        public static int GetDiscountPercentage(Boolean isEmployee, float subtotal, int amountOfBooks)
        {
            if ((subtotal > 100) || (isEmployee && (amountOfBooks > 3)))
            {
                return 10;
            }
            if (isEmployee || (amountOfBooks > 3))
            {
                return 5;
            }
            return 0;
        }

        public static float CalculateTotal(int discount, float subtotal)
        {
            return subtotal * (100 - discount) / 100;
        }
    }
}
