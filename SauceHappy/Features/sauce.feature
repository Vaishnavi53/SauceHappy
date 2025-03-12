Feature: sauce

A short summary of the feature



Scenario: Sauce demo
	Given user is on the login page
	When user enters the "<usrname>" and "<passwd>" in the text fields
	When user clicks submit button
	Then user is navigated to home page

	When the user clicks the product wants
	And user clicks the add to cart button 
	And user clicks cart icon
	Then cart should show the product added

	When user proceeds to checkout
	And enters "<first name>"  "<last name>" and "<postal code>"
	And user continues by clicking continue
	And user click finish
	Then the message Thankyou for your order! should be displayed 

Examples: 
| usrname       | passwd       | first name | last name   | postal code |
| standard_user | secret_sauce | vaishnavi  | manjalagiri | 671552      |

 

