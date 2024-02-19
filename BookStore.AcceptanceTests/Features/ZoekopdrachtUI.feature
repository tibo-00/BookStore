
Feature: SearchbarUI

The searchbar helps users find books based on title, author, genre. 

Background:
Given I am using the web app
And I am on the book catalog page

@hoofdscenario
Scenario: Search for a book that exists with a title using the checkbox and genre
When I check the byTitle checkbox
And I search for a book with the title: BDD in action second edition
When I select Action as Genre
And click submit
Then I should see the book in the catalog list

@title
Scenario: Search for a book that exists with a title
When I search for a book with the title: BDD in action second edition
And click submit
Then I should see the book in the catalog list

@author
Scenario: Search for a book that exists with an author
When I search for a book with the author: John Ferguson Smart
And click submit
Then I should see the book in the catalog list

@titlecheckbox
Scenario: Search for a book that exists with a title using the checkbox
When I check the byTitle checkbox
And I search for a book with the title: BDD in action second edition
And click submit
Then I should see the book in the catalog list

@authorcheckbox
Scenario: Search for a book that exists with an author using the checkbox
When I check the byAuthor checkbox
And I search for a book with the author: John Ferguson Smart
And click submit
Then I should see the book in the catalog list

@titlegenre
Scenario: Search for a book that exists with a title using the genre
When I select Action as Genre
And I search for a book with the title: BDD in action second edition
And click submit
Then I should see the book in the catalog list

@authorgenre
Scenario: Search for a book that exists with an author using the genre
When I select Action as Genre
And I search for a book with the author: John Ferguson Smart
And click submit
Then I should see the book in the catalog list

@checkbox
Scenario: Search only using the checkbox
When I check the byAuthor checkbox
And click submit
Then I should see books in the catalog list

@wrongtitle
Scenario: Search for a book that does not exist with a title
When I search for a book with the title: Aan de slag met software testen
And click submit
Then I should not see the book in the catalog list

@wrongauthor
Scenario: Search for a book that does not exist with an author
When I search for a book with the author: Hossein Chamani
And click submit
Then I should not see the book in the catalog list

@wrongtitlecheckbox
Scenario: Search for a book that does not exist with a title using the checkbox
When I check the byTitle checkbox
And I search for a book with the title: Aan de slag met software testen
And click submit
Then I should not see the book in the catalog list

@wrongauthorcheckbox
Scenario: Search for a book that does not exist with an author using the checkbox
When I check the byAuthor checkbox
And I search for a book with the author: Hossein Chamani
And click submit
Then I should not see the book in the catalog list

@titlewrongcheckbox
Scenario: Search for a book that exists with a title using the wrong checkbox
When I check the byAuthor checkbox
And I search for a book with the title: BDD in action second edition
And click submit
Then I should not see the book in the catalog list

@authorwrongcheckbox
Scenario: Search for a book that exists with an author using the wrong checkbox
When I check the byTitle checkbox
And I search for a book with the author: John Ferguson Smart
And click submit
Then I should not see the book in the catalog list

@genre
Scenario: Search for a book based on the genre
When I select Action as Genre
And click submit
Then I should see books in the catalog list with category Action

@genrenobooks
Scenario: Search for a book based on the genre with no books
When I select Horror as Genre
And click submit
Then I should not see any books in the catalog list
