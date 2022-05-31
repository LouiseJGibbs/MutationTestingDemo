
Feature: AdminInbox
	As an admin user, I want to read messages customers have sent me 

Scenario Outline: Read contact us message sent via Contact Us form
Given valid contact details have been submitted via the contact us form
And the user has logged into the admin section
	When the admin inbox is opened
		Then the contact us message can be found in the list of unread messages
	When the message is opened
		Then the message window should appear containing the contact us message details
