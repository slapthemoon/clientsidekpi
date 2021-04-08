Feature: Search

Scenario: 1. Validate Search with Valid data
	Given I open main view
	When I search 'Комп'ютер HP 290 G1 MT (3ZD04EA)' value
	Then I see 'Компьютер HP 290 G1 MT (3ZD04EA)' result