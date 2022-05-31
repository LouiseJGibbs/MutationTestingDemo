Feature: Contact Us
	I want to be able to contact the business

Scenario Outline: Complete Contact Us Form with valid settings
Given the contact us form is open
And some random valid contact us data is generated
When the contact us form is completed
And the submit button is clicked on the contact us form
Then a message should appear saying that the contact us form was successfully submitted