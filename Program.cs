using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

class Program
{
    static void Main(string[] args)
    {
        // Create ChromeOptions
        var options = new ChromeOptions();

        // --------------------------------------------------------------------
        // Default Chrome user data directory for macOS:
        // /Users/<YourMacUsername>/Library/Application Support/Google/Chrome (type whoami in mac terminal to find YourMacUsername)
        // --------------------------------------------------------------------
        // EXAMPLE (COMMENTED OUT) FOR MAC:
        // options.AddArguments(
        //     "user-data-dir=/Users/user/Library/Application Support/Google/Chrome",
        //     "profile-directory=Default"
        // );

        // --------------------------------------------------------------------
        // EXAMPLE  FOR WINDOWS:
        // Typically located at C:\Users\<YourWindowsUsername>\AppData\Local\Google\Chrome\User Data
        //
        options.AddArguments(
            "user-data-dir=C:\\Users\\<YourWindowsUsername>\\AppData\\Local\\Google\\Chrome\\User Data",
            "profile-directory=Default"
        );
        // --------------------------------------------------------------------

        // Initialize ChromeDriver
        IWebDriver driver = new ChromeDriver(options);

        // Google Form URL
        string formUrl = "ENTER GOOGLE FORM URL HERE";
        string yourName = "Jane Doe";

        bool formSubmitted = false;

        while (!formSubmitted)
        {
            try
            {
                driver.Navigate().GoToUrl(formUrl);

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(0.1))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(100)
                };

                // Locate the text/textarea field
                IWebElement inputField = wait.Until(drv =>
                {
                    var element = FindElementSafely(drv, By.XPath("//input[@type='text']"))
                                  ?? FindElementSafely(drv, By.XPath("//textarea"));
                    return (element != null && element.Displayed && element.Enabled) ? element : null;
                });
                ScrollToElement(driver, inputField);
                inputField.SendKeys(yourName);

                // Optionally check "Record email" checkbox if it exists
                IWebElement recordEmailCheckbox = FindElementSafely(driver,
                    By.XPath("//div[@role='checkbox' and contains(@aria-label, 'Record ')]"));

                if (recordEmailCheckbox != null && recordEmailCheckbox.Displayed && recordEmailCheckbox.Enabled)
                {
                    // Check if it's already selected
                    var isChecked = recordEmailCheckbox.GetAttribute("aria-checked");
                    if (isChecked == "false")
                    {
                        ScrollToElement(driver, recordEmailCheckbox);
                        recordEmailCheckbox.Click();
                        Console.WriteLine("Clicked the 'Record email' checkbox.");
                    }
                }

                // Locate and click the Submit button
                IWebElement submitButton = wait.Until(drv =>
                {
                    var element = FindElementSafely(drv, By.XPath("//span[contains(text(), 'Submit')]"));
                    return (element != null && element.Displayed && element.Enabled) ? element : null;
                });
                ScrollToElement(driver, submitButton);
                submitButton.Click();

                Console.WriteLine("Form submitted successfully!");
                formSubmitted = true;
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Form not ready yet. Retrying at {DateTime.Now}...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                break;
            }
        }

        // Remove or comment out driver.Quit() if you want the browser to stay open
        // driver.Quit();

        Console.WriteLine("Press ENTER to close...");
        Console.ReadLine();
        driver.Quit();
    }

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

    static void ScrollToElement(IWebDriver driver, IWebElement element)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
    }
}
