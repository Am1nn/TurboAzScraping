using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TurboAzScraping.Services
{
    public class VehiclePageGenerator
    {
        public ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
        public ChromeOptions chromeOptions = new ChromeOptions();
        public ICollection<string>? PageLinks { get; set; }
        public ICollection<string>? VehicleLinks { get; set; }


        public VehiclePageGenerator()
        {
            PageLinks = new List<string>();
            VehicleLinks = new List<string>();
        }


        public ICollection<string>? GeneratePages(int pageCount)
        {
            PageLinks = new List<string>();
            // Maximum 30 page !! minimum 1 page
            if (pageCount < 1) { pageCount = 1; }
            //if (pageCount > 30) { pageCount = 1; }
            string link = "https://turbo.az/autos?page=";
            for (int i = 1; i <= pageCount; i++)
            {
                PageLinks?.Add(link + $"{i}");
            }
            return PageLinks;
        }

        public ICollection<string>? GenerateVehicleLinks(ICollection<string> pageLinks)
        {



            using (var driver = new ChromeDriver(chromeDriverService, chromeOptions))
            {
                try
                {
                    int counter = 1;
                    foreach (var pageLink in pageLinks)
                    {
                        driver.Navigate().GoToUrl(pageLink);

                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                        IList<IWebElement> productLinks = driver.FindElements(By.ClassName("products-i__link"));


                        foreach (var link in productLinks)
                        {
                            string hrefValue = link.GetAttribute("href");

                            // Vehicle Links
                            VehicleLinks?.Add(hrefValue);
                            counter++;
                        }
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
            return VehicleLinks;
        }

    }
}
