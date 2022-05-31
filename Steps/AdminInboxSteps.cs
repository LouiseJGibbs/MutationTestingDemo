using NUnit.Framework;
using OpenQA.Selenium;
using RestfulBookerSpecflowUITests;
using RestfulBookerUITests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace RestfulBookerUITests.Steps
{
    [Binding]
    public sealed class AdminInboxSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        private readonly ScenarioContext scenarioContext;
        private readonly IWebDriver driver;
        private readonly Data testData;

        AdminInboxControls adminInboxControls => new AdminInboxControls(driver);
        LoginControls loginControls => new LoginControls(driver);

        IWebElement MessageFromList;
        public AdminInboxSteps(ScenarioContext scenarioContext, IWebDriver driver, Data testData)
        {
            this.scenarioContext = scenarioContext;
            this.driver = driver;
            this.testData = testData;
        }

        [Given(@"the user has logged into the admin section")]
        public void GivenTheUserHasLoggedIntoTheAdminSection()
        {
            loginControls.AdminLink.Click();

            loginControls.UsernameTextbox.SendKeys("admin");
            loginControls.PasswordTextbox.SendKeys("password");
            loginControls.LoginButton.Click();
        }

        [When(@"the admin inbox is opened")]
        public void WhenViewTheEmailInbox()
        {
            adminInboxControls.MessagesInboxLink.Click();
        }

        [Then(@"the contact us message can be found in the list of unread messages")]
        public void ThenICanSeeTheNameAndSubjectInTheListOfUnreadMessages()
        {
            bool found = false;

            for (int i = adminInboxControls.MessagesList_Unread.Count; i > 0; i--)
            {
                IWebElement message = adminInboxControls.MessagesList_Unread[i - 1];
                string text = message.Text;

                if (text.Contains(testData.MyMessage.Name) && text.Contains(testData.MyMessage.Subject))
                {
                    found = true;
                    MessageFromList = message;
                    break;
                }
            }

            Assert.True(found, "The message could not be found in the list of read messages");
        }

        [Then(@"the room booking message can be found in the list of unread messages")]
        public void ThenRoomBookingMessageCanBeFoundInTheListOfUnreadMessages()
        {
            bool found = false;

            for (int i = adminInboxControls.MessagesList_Unread.Count; i > 0; i--)
            {
                IWebElement message = adminInboxControls.MessagesList_Unread[i - 1];
                string text = message.Text;

                if (text.Contains(testData.MyRoomBooking.FirstName) && text.Contains(testData.MyRoomBooking.LastName))
                {
                    found = true;
                    MessageFromList = message;
                    break;
                }
            }

            Assert.True(found, "The message could not be found in the list of read messages");
        }

        [Then(@"I can see the contact us message in the list of read messages")]
        public void ThenICanSeeTheNameAndSubjectInTheListOdReadMessages()
        {
            bool found = false;

            for (int i = adminInboxControls.MessagesList_Read.Count; i > 0; i--)
            {
                IWebElement message = adminInboxControls.MessagesList_Read[i - 1];
                string text = message.Text;

                if (text.Contains(testData.MyMessage.Name) && text.Contains(testData.MyMessage.Subject))
                {
                    found = true;
                    MessageFromList = message;
                    break;
                }
            }

            Assert.True(found, "The message could not be found in the list of Unread messages");
        }

        [When(@"the message is opened")]
        public void WhenIClickOnTheMessage()
        {
            MessageFromList.Click();
        }

        [Then(@"the message window should appear containing the contact us message details")]
        public void ThenDetailsOfTheMessageCanBeViewed()
        {
            Assert.True(adminInboxControls.MessageWindowBy.Exists(driver), "The message window is not available");

            string textSearchString = ".//*[text()='{0}']";

            By PhoneNumberText = By.XPath(String.Format(textSearchString, testData.MyMessage.PhoneNumber));
            By NameText = By.XPath(String.Format(textSearchString, testData.MyMessage.Name));
            By EmailText = By.XPath(String.Format(textSearchString, testData.MyMessage.Email));
            By SubjectText = By.XPath(String.Format(textSearchString, testData.MyMessage.Subject));
            By MessageText = By.XPath(String.Format(textSearchString, testData.MyMessage.Message));


            var MessageDetails = adminInboxControls.MessageDetails;
            Assert.True(PhoneNumberText.Exists(MessageDetails[0]), "The phone number was not found in the expected location");
            Assert.True(NameText.Exists(MessageDetails[0]), "The name was not found in the expected location");
            Assert.True(EmailText.Exists(MessageDetails[1]), "The email was not found in the expected location");
            Assert.True(SubjectText.Exists(MessageDetails[2]), "The subject was not found in the expected location");
            Assert.True(MessageText.Exists(MessageDetails[3]), "The message was not found in the expected location");
        }

        [Then(@"the message window should appear containing the room booking details")]
        public void ThenDetailsOfRoomBookShouldBeInMessage()
        {
            Assert.True(adminInboxControls.MessageWindowBy.Exists(driver), "The message window is not available");

            string textSearchString = ".//*[text()='{0}']";

            By PhoneNumberText = By.XPath(String.Format(textSearchString, testData.MyRoomBooking.PhoneNumber));
            By NameText = By.XPath(String.Format(textSearchString, $"{testData.MyRoomBooking.FirstName} {testData.MyRoomBooking.LastName}"));
            By EmailText = By.XPath(String.Format(textSearchString, testData.MyRoomBooking.Email));
            By SubjectText = By.XPath(String.Format(textSearchString, "Booking enquiry"));
            By MessageText = By.XPath(String.Format(textSearchString, "I would like to book a room at your place"));


            var MessageDetails = adminInboxControls.MessageDetails;
            Assert.True(PhoneNumberText.Exists(MessageDetails[0]), "The phone number was not found in the expected location");
            Assert.True(NameText.Exists(MessageDetails[0]), "The name was not found in the expected location");
            Assert.True(EmailText.Exists(MessageDetails[1]), "The email was not found in the expected location");
            Assert.True(SubjectText.Exists(MessageDetails[2]), "The subject was not found in the expected location");
            Assert.True(MessageText.Exists(MessageDetails[3]), "The message was not found in the expected location");
        }
    }
}
