using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace RestfulBookerUITests.Steps
{
    [Binding]
    public sealed class BookRoomControls
    {
        private readonly IWebDriver driver;

        public BookRoomControls(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement BookRoomButton => this.driver.FindElement(By.XPath("//button[@class='btn btn-outline-primary float-right openBooking']"));
        public ReadOnlyCollection<IWebElement> RoomInformation => this.driver.FindElements(By.XPath("//div[@class='row hotel-room-info']"));
        public int InitialRoomSectionsOnPage = 0;

        public IWebElement Calendar => this.driver.FindElement(By.XPath("//div[@class='rbc-calendar']"));
        public ReadOnlyCollection<IWebElement> CalendarRows => Calendar.FindElements(By.XPath(".//div[@class='rbc-month-row']"));
        public By DaysInWeek => By.XPath(".//div[@class='rbc-day-bg']");

        public IWebElement FirstNameTextbox => this.driver.FindElement(By.Name("firstname"));
        public IWebElement LastNameTextbox => this.driver.FindElement(By.Name("lastname"));
        public IWebElement EmailTextbox => this.driver.FindElement(By.Name("email"));
        public IWebElement PhoneTextbox => this.driver.FindElement(By.Name("phone"));
        public IWebElement SubmitBookingButton => this.driver.FindElement(By.XPath("//button[@class='btn btn-outline-primary float-right book-room']"));
        public IWebElement NextMonth => this.driver.FindElement(By.XPath("//button[text()='Next']"));
        public IWebElement SuccessfulBookingMessage => this.driver.FindElement(By.XPath("//div[@class='form-row']//h3"));
    }
}

