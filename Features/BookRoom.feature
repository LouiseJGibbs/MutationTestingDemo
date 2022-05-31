Feature: BookRoom
	As an customer, I want to be able to book a room in the hotel

Scenario: Book a room
Given some random valid room booking details are generated
And at least 1 room exists in the hotel
	When the book a room button is clicked
		Then the room info section should appear
	When a valid date range is selected from the book room calendar
	And the book room form is completed and submitted
		Then the successful room booking message should appear
