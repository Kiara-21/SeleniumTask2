using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Threading;

class PastebinAutomation
{
    static void Main(string[] args)
    {
        IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://pastebin.com/");

        IWebElement textBox = driver.FindElement(By.XPath("//*[@id=\"postform-text\"]"));
        var text = "git config --global user.name  \"New Sheriff in Town\"\r\ngit reset $(git commit-tree HEAD^{tree} -m \"Legacy code\")\r\ngit push origin master --force\r\n";
        textBox.SendKeys(text);

        var js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("window.scrollBy(0,500)", "");

        IWebElement dropExpire = driver.FindElement(By.XPath("//*[contains(@class, 'field-postform-expiration')]//span"));
        dropExpire.Click();

        var dropExpireOptions = driver.FindElements(By.CssSelector("li[class *= 'select2-results__option']"));
        var dropExpireOptionsElement = dropExpireOptions.FirstOrDefault(e => e.Text.Contains("10 Minutes"));
        dropExpireOptionsElement.Click();

        IWebElement dropSyntax = driver.FindElement(By.XPath("//*[contains(@class, 'field-postform-format')]//span"));
        dropSyntax.Click();

        var dropSyntaxOptions = driver.FindElements(By.CssSelector("#select2-postform-format-results > li:nth-child(2) > ul > li"));
        var dropSyntaxOptionsElement = dropSyntaxOptions.FirstOrDefault(e => e.Text.Contains("Bash"));
        dropSyntaxOptionsElement.Click();

        IWebElement textTitle = driver.FindElement(By.XPath("//*[@id=\"postform-name\"]"));
        textTitle.SendKeys("how to gain dominance among developers");

        IWebElement sendButton = driver.FindElement(By.XPath("//*[@id=\"w0\"]/div[5]/div[1]/div[10]/button"));
        sendButton.Click();

        if ("how to gain dominance among developers - Pastebin.com" == driver.Title)
        {
            Console.WriteLine("Test1 - passed");
        }
        else
        {
            Console.WriteLine("Test1 - failed");
        }

        IWebElement syntaxElement = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[1]/div[1]/div[4]/div[1]/div[1]/a[1]"));
        var syntaxText = syntaxElement.Text;

        if ("Bash" == syntaxText)
        {
            Console.WriteLine("Test2 - passed");
        }
        else
        {
            Console.WriteLine("Test2 - failed");
        }

        IWebElement mainContent = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[1]/div[1]/div[4]/div[2]/ol"));
        var mainText = mainContent.Text;
       
        if (text == mainText)
        {
            Console.WriteLine("Test3 - passed");
        }
        else
        {
            Console.WriteLine("Test3 - failed");
        }

        Thread.Sleep(5000);
        driver.Quit();
    }
}