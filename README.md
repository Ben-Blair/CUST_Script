 STEPS:

install DotNet 9.0
     https://dotnet.microsoft.com/en-us/download/dotnet?cid=getdotnetcorecli


TYPE INTO TERMINAL ON VS CODE:
      dotnet add package Selenium.WebDriver --version 4.28.0
      dotnet add package Selenium.Support --version 4.28.0
      dotnet add package Selenium.WebDriver.ChromeDriver --version 132.0.6834.8300


Change name and Google form link URL on Program.cs file (Line 15 & 16)


HOW TO RUN:
          Option 1:
            While Program.cs is displayed, press down arrow button to the right of the run button (looks like play button located top right of VS Code screen)
            Press "Run Project Associated With This File"
            --------------------------------------------------------------------------------------------
          Option 2:
            cd to location of FormFiller (In Terminal)
            dotnet build (In Terminal)
            dotnet run (In Terminal)
