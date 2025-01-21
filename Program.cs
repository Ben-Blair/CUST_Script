using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

class Program
{
    static void Main(string[] args)
    {
        // Initialize ChromeDriver
        using IWebDriver driver = new ChromeDriver();

        // Google Form URL ENTER YOUR INFORMATION HERE
        //--------------------------------------------//
        string formUrl = "https://docs.google.com/forms/d/e/1FAIpQLMsDdoaHblYXNqkQjsqKDHUFK-7G0YwccQiKn01vKw/viewform?usp=dialog";
        string yourName = "John Smith";
        //--------------------------------------------//

        bool formSubmitted = false;

        while (!formSubmitted)
        {
            try
            {
                // Navigate to the form URL
                driver.Navigate().GoToUrl(formUrl);

                // Wait for the input field to be interactable dynamically
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(0.2))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(100) // Poll every 100ms
                };

                IWebElement inputField = wait.Until(driver =>
                {
                    var element = FindElementSafely(driver, By.XPath("//input[@type='text']")) ??
                                  FindElementSafely(driver, By.XPath("//textarea"));
                    return (element != null && element.Displayed && element.Enabled) ? element : null;
                });

                // Scroll to the input field and enter your name
                ScrollToElement(driver, inputField);
                inputField.SendKeys(yourName);

                // Wait for the Submit button to be interactable dynamically
                IWebElement submitButton = wait.Until(driver =>
                {
                    var element = FindElementSafely(driver, By.XPath("//span[contains(text(), 'Submit')]"));
                    return (element != null && element.Displayed && element.Enabled) ? element : null;
                });

                // Scroll to the Submit button and click it
                ScrollToElement(driver, submitButton);
                submitButton.Click();

                Console.WriteLine("Form submitted successfully!");
                formSubmitted = true; // Break the loop after submission
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Form not ready yet. Retrying dynamically at {DateTime.Now}...");
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

    // Helper method to find an element safely
    static IWebElement FindElementSafely(IWebDriver driver, By by)
    {
        try
        {
            return driver.FindElement(by);
        }
        catch (NoSuchElementException)
        {
            return null;
        }
    }

    // Helper method to scroll to an element
    static void ScrollToElement(IWebDriver driver, IWebElement element)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
    }
}
