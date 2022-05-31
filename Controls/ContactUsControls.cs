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
    public sealed class ContactUsControls
    {
        private readonly IWebDriver driver;

        public ContactUsControls(IWebDriver driver)
        {
            this.driver = driver;
        }

        public By ContactUsSectionBy => By.XPath(".//div[@class='row contact']//div[@class='col-sm-5']");
        public IWebElement ContactUsSection => this.driver.FindElement(ContactUsSectionBy);

        //Contact form
        public By FormSectionBy => By.XPath("//form");
        public IWebElement FormSection => ContactUsSection.FindElement(FormSectionBy);
        public IWebElement NameTextBox => FormSection.FindElement(By.Id("name"));
        public IWebElement EmailTextBox => FormSection.FindElement(By.Id("email"));
        public IWebElement PhoneTextBox => FormSection.FindElement(By.Id("phone"));
        public IWebElement SubjectTextBox => FormSection.FindElement(By.Id("subject"));
        public IWebElement MessageTextBox => FormSection.FindElement(By.Id("description"));
        public IWebElement SubmitButton => FormSection.FindElement(By.Id("submitContact"));

        //Confirmation message
        public IWebElement HeaderText => ContactUsSection.FindElement(By.XPath(".//h2"));
        public IReadOnlyCollection<IWebElement> ParagraphText => ContactUsSection.FindElements(By.XPath(".//p"));

    }
}

