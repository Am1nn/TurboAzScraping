using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TurboAzScraping.Services
{
    public class DealershipPageGenerator
    {
        public ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
        public ChromeOptions chromeOptions = new ChromeOptions();
        public ICollection<string>? DealershipLiks { get; set; }

        public DealershipPageGenerator()
        {
            DealershipLiks = new HashSet<string>();
        }


        public ICollection<string>? GenerateDealershipLinks(bool isFeatured)
        {
            string className = "shops-i";
            if (isFeatured) { className = "featured"; }
            using (var driver = new ChromeDriver(chromeDriverService, chromeOptions))
            {
                try
                {

                    driver.Navigate().GoToUrl("https://turbo.az/avtosalonlar");

                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                    IList<IWebElement> dealershipLinks = driver.FindElements(By.ClassName(className));

                    int counter = 1;
                    foreach (var link in dealershipLinks)
                    {
                        string hrefValue = link.GetAttribute("href");

                        // Vehicle Links
                        Console.WriteLine(counter+") "+hrefValue);
                        DealershipLiks?.Add(hrefValue);
                        counter++;
                    }
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Wrong Link Or Empty Elements!!!");
                }
                finally
                {
                    driver.Quit();
                }
            }
            return DealershipLiks;
        }








    }
}
