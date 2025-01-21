using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static void Main(string[] args)
    {
        // Initialize ChromeDriver
        using IWebDriver driver = new ChromeDriver();

        // Google Form URL ENTER YOUR INFORMATION HERE
        //--------------------------------------------//
        string formUrl = "https://forms.gle/SNjbknJbR8nczhZ29";
        string yourName = "Ben Blair";
        //--------------------------------------------//

        bool formSubmitted = false;

        while (!formSubmitted)
        {
            try
            {
                // Navigate to the form URL
                driver.Navigate().GoToUrl(formUrl);

                // Locate the input field for the short answer or paragraph question
                IWebElement inputField;
                try
                {
                    // Try finding a short answer input field first
                    inputField = driver.FindElement(By.XPath("//input[@type='text']"));
                }
                catch (NoSuchElementException)
                {
                    // If no short answer input field is found, look for a paragraph field
                    inputField = driver.FindElement(By.XPath("//textarea"));
                }

                // Enter your name
                inputField.SendKeys(yourName);

                // Locate the Submit button
                IWebElement submitButton = driver.FindElement(By.XPath("//span[contains(text(), 'Submit')]"));

                // Click the Submit button
                submitButton.Click();

                Console.WriteLine("Form submitted successfully!");
                formSubmitted = true; // Break the loop after submission
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"Form not available yet. Retrying instantly at {DateTime.Now}...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                break;
            }
        }

        // Close the browser
        driver.Quit();
    }
}
