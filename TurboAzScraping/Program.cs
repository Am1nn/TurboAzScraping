using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TurboAzScraping.Services;

namespace TurboAzScraping
{
    public class Program
    {
        static void Main(string[] args)
        {

            //VehiclePageGenerator vp = new VehiclePageGenerator();
            //ICollection<string> rf = vp.GeneratePages(100);
            //vp.GenerateVehicleLinks(rf);
            
            DealershipPageGenerator dp=new DealershipPageGenerator();
            dp.GenerateDealershipLinks(true);





        }
    }
}
