Feature: ManageAuthors

Users with the permission "CanManageCatalog" are able to Create, Edit, and Delete Authors. 
To Create or Edit every field has to be filled.
To delete an Author all the books from that author need to be deleted first.


@tag1
Scenario: User creates a new author
    Given the user has the "CanManageCatalog" permission
    When the user navigates to create a new author
    And fills in all the required fields for an author
    Then the new author is successfully created
    And the new author is visible in the overview list of authors

#@tag2
#Scenario: User edits an existing author
#    Given the user has the "CanManageCatalog" permission
#    And there is an existing author
#    When the user edits the author's details
#    And updates all the necessary fields
#    Then the author's information is successfully updated
#
#@tag3
#Scenario: User tries to delete an author with associated books
#    Given the user has the "CanManageCatalog" permission
#    And there is an existing author with associated books
#    When the user attempts to delete the author
#    Then the books stays visible in the overview list of authors
#
#@tag4
#Scenario: User deletes an author with that has no associated books
#    Given the user has the "CanManageCatalog" permission
#    And there is an existing author with no associated books
#    When the user attempts to delete the author
#    Then the  auhtor is not visible anymore in the overview list of authors
