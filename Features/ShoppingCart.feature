Feature: ShoppingCart flow

Validate adding and removing items from a cart

Scenario: Validate adding and removing items from a cart
	Given I access the katalon website
	When I add four random items to my cart
	When I find total four items listed in my cart
	When I search for the lowest price item
	And I am able to remove the lowest price item from my cart
	Then I am able to verify three items in my cart
