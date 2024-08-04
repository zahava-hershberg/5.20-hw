using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scraper.Web.Services;

namespace Scraper.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LakewoodScoop : ControllerBase
    {
        [Route("Scrape")]
        public List<News> Scrape()
        {
            var scraper = new LakewoodScoopScraper();
            return scraper.Scrape();

        }
    }
}
