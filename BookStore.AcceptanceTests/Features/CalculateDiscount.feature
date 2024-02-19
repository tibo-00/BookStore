Feature: Calculate Discount of an order

Calculate which discount to give on a purchase based on the number of books, the total cost, and an additional discount for an employee

@tag1
Scenario: Customer buys books for more than 100 euro
Given the user is a customer
When the user checks out with a subtotal of 120 euro and 2 books
Then the discount should be 10%, resulting in a total of 108 euro

@tag2
Scenario: Employee buys more than three books for less than 100 euro
Given the user is an employee
When the user checks out with a subtotal of 40 euro and 4 books
Then the discount should be 10%, resulting in a total of 36 euro

@tag3
Scenario: Customer buys more than three books for less than 100 euro
Given the user is a customer
When the user checks out with a subtotal of 95 euro and 5 books
Then the discount should be 5%, resulting in a total of 90.25 euro

@tag4
Scenario: Employee buys three or fewer books for less than 100 euro
Given the user is an employee
When the user checks out with a subtotal of 24 euro and 3 books
Then the discount should be 5%, resulting in a total of 22.8 euro

@tag5
Scenario: Customer buys three or fewer books for less than 100 euro
Given the user is a customer
When the user checks out with a subtotal of 19 euro and 1 book
Then the discount should be 0%, resulting in a total of 19 euro
