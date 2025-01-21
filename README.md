# FormFiller Automation Script

This project automates the process of interacting with a Google Form using Selenium WebDriver in C#. Follow the steps below to set up and run the project.

## Prerequisites

1. Install **.NET 9.0**  
   [Download .NET 9.0 here](https://dotnet.microsoft.com/en-us/download/dotnet?cid=getdotnetcorecli)

## Installation

1. Open the terminal in **Visual Studio Code** and navigate to the project directory.
2. Run the following commands in the terminal to add the required packages:
   ```bash
   dotnet add package Selenium.WebDriver --version 4.28.0
   dotnet add package Selenium.Support --version 4.28.0
   dotnet add package Selenium.WebDriver.ChromeDriver --version 132.0.6834.8300

Make sure the version for "Selenium.WebDriver.ChromeDriver" is the same as your Chrome-based browser. Mine is version 132.0.6834.8300

To find the version, press the three lines at the top right of your browser, press help, then about.
