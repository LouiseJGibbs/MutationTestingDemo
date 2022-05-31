using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using RestfulBookerSpecflowUITests;
using RestfulBookerUITests.TestData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace RestfulBookerUITests.Steps
{
    [Binding]
    public sealed class BookRoomSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext scenarioContext;
        private readonly IWebDriver driver;
        private readonly Data testData;

        BookRoomControls bookRoomControls => new BookRoomControls(driver);

        int InitialRoomSectionsOnPage;

        public BookRoomSteps(ScenarioContext scenarioContext, IWebDriver driver, Data testData)
        {
            this.scenarioContext = scenarioContext;
            this.driver = driver;
            this.testData = testData;
        }

        [Given(@"a room booking has been created")]
        public void GivenARoomBookingHasBeenCreated()
        {
            GenerateRandomRoomBookingDetails();
            ClickBookRoomButton();
            SelectDateRangeFromCalendar();
            CompleteAndSubmitBookRoomForm();
        }


        [Given(@"some random valid room booking details are generated")]
        public void GenerateRandomRoomBookingDetails()
        {
            testData.MyRoomBooking.FirstName = TestUtilities.GenerateRandomString(10);
            testData.MyRoomBooking.LastName = TestUtilities.GenerateRandomString(10); 
            testData.MyRoomBooking.PhoneNumber = $"0{TestUtilities.GenerateRandomNumber(11)}";
            testData.MyRoomBooking.Email = $"{TestUtilities.GenerateRandomString(8)}@{TestUtilities.GenerateRandomString(8)}.com"; 
        }

        [Given(@"at least 1 room exists in the hotel")]
        public void CheckARoomExists()
        {
            Assert.True(bookRoomControls.RoomInformation.Count > 0, "No rooms currently exist in the hotel");
        }

        [When(@"the book a room button is clicked")]
        public void ClickBookRoomButton()
        {
            InitialRoomSectionsOnPage = bookRoomControls.RoomInformation.Count; //When room section expands, an additional section is created on the page
            bookRoomControls.BookRoomButton.Click();
        }

        [Then(@"the room info section should appear")]
        public void CheckRoomInfoSectionAppears()
        {
            Assert.True(bookRoomControls.RoomInformation.Count > InitialRoomSectionsOnPage);
        }


        [When(@"a valid date range is selected from the book room calendar")]
        public void SelectDateRangeFromCalendar()
        {

            bool dateSet = false;
            bool available = false;

            while (!dateSet)
            {
                foreach (IWebElement week in bookRoomControls.CalendarRows)
                {
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                    try
                    {
                        available = !week.FindElement(By.XPath(".//div[@title='Unavailable']")).Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        available = true;
                    }

                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TestUtilities.Timeout);

                    //Only book week if there are more than 1 day within the current month, and the week is available
                    if (available && week.FindElements(bookRoomControls.DaysInWeek).Count > 1)
                    {
                        week.ScrollToElement(driver);
                        SelectDateRangeInWeek(week);
                        dateSet = true;
                        break;
                    }
                }

                bookRoomControls.NextMonth.Click();
            }

        }

        public void SelectDateRangeInWeek(IWebElement week)
        {
            ReadOnlyCollection<IWebElement> days = week.FindElements(bookRoomControls.DaysInWeek);
            IWebElement day1 = days[0];
            IWebElement day2 = days[days.Count - 1];

            var action = new Actions(driver);

            action.ClickAndHold(day2);
            action.MoveToElement(day2);
            action.MoveToElement(day1);
            action.DragAndDrop(day1, day2);
            action.Perform();
        }

        [When(@"the book room form is completed and submitted")]
        public void CompleteAndSubmitBookRoomForm()
        {
            bookRoomControls.FirstNameTextbox.SendKeys(testData.MyRoomBooking.FirstName);
            bookRoomControls.LastNameTextbox.SendKeys(testData.MyRoomBooking.LastName);
            bookRoomControls.EmailTextbox.SendKeys(testData.MyRoomBooking.Email);
            bookRoomControls.PhoneTextbox.SendKeys(testData.MyRoomBooking.PhoneNumber);
            bookRoomControls.SubmitBookingButton.Click();
        }

        [Then(@"the successful room booking message should appear")]
        public void ThenIShouldSeeTheSuccessfulBookingMessage()
        {
            Assert.AreEqual("Booking Successful!", bookRoomControls.SuccessfulBookingMessage.Text);
        }
    }
}
