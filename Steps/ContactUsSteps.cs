using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RestfulBookerSpecflowUITests;
using RestfulBookerUITests.TestData;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace RestfulBookerUITests.Steps
{
    [Binding]
    public sealed class ContactUsSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext scenarioContext;
        private readonly IWebDriver driver;
        private readonly Data testData;

        ContactUsControls contactUsControls => new ContactUsControls(this.driver);

        public ContactUsSteps(ScenarioContext scenarioContext, IWebDriver driver, Data testData)
        {
            this.scenarioContext = scenarioContext;
            this.driver = driver;
            this.testData = testData;
        }

        [Given(@"valid contact details have been submitted via the contact us form")]
        public void GivenValidContactDetailsHaveBeenSubmittedViaTheContactUsForm()
        {
            GenerateRandomContactUsInfo();
            CompleteContactUsForm();
            SubmitContactUsForm();
        }

        [Given(@"the contact us form is open")]
        public void GivenTheContactUsFormIsOpen()
        {
            this.contactUsControls.ContactUsSection.ScrollToElement(this.driver);
            Assert.True(contactUsControls.ContactUsSectionBy.Exists(this.driver));
        }

        [Given(@"some random valid contact us data is generated")]
        public void GenerateRandomContactUsInfo()
        {
            testData.MyMessage.Name = TestUtilities.GenerateRandomString(10);
            testData.MyMessage.Email = $"{TestUtilities.GenerateRandomString(8)}@{TestUtilities.GenerateRandomString(8)}.com";
            testData.MyMessage.PhoneNumber = $"0{TestUtilities.GenerateRandomNumber(11)}";
            testData.MyMessage.Subject = TestUtilities.GenerateRandomAlphanumericString(10);
            testData.MyMessage.Message = TestUtilities.GenerateRandomAlphanumericString(50);
        }

        [When(@"the contact us form is completed")]
        public void CompleteContactUsForm()
        {
            this.contactUsControls.NameTextBox.SendKeys(testData.MyMessage.Name);
            this.contactUsControls.EmailTextBox.SendKeys(testData.MyMessage.Email);
            this.contactUsControls.PhoneTextBox.SendKeys(testData.MyMessage.PhoneNumber);
            this.contactUsControls.SubjectTextBox.SendKeys(testData.MyMessage.Subject);
            this.contactUsControls.MessageTextBox.SendKeys(testData.MyMessage.Message);
        }

        [When(@"the submit button is clicked on the contact us form")]
        public void SubmitContactUsForm()
        {
            this.contactUsControls.SubmitButton.Click();
        }

        [Then(@"a message should appear saying that the contact us form was successfully submitted")]
        public void CheckMessageAppearsAfterSubmittingContactUsForm()
        {
            var expectedText = String.Format("Thanks for getting in touch {0}!", testData.MyMessage.Name);
            WebDriverWait wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10));
            var checkMessage = wait.Until(ExpectedConditions.TextToBePresentInElement(this.contactUsControls.HeaderText, expectedText));
            Assert.True(checkMessage);

            string paragraph = "";
            foreach (IWebElement p in this.contactUsControls.ParagraphText)
                paragraph += p.Text + " ";

            Assert.AreEqual(String.Format("We'll get back to you about {0} as soon as possible. ", testData.MyMessage.Subject), paragraph);
        }
    }
}
