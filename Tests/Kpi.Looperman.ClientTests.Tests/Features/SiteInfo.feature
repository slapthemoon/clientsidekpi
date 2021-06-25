@Regression
@SiteInfo
Feature: Site Info
	Verify the posibility to see site info

@Smoke
Scenario: 1. Verify the possibility to see the information about site
	Given I have opened main page
	When I click What is Looperman ? button
	Then I see 'So, What Is looperman.com ?' as title of the page