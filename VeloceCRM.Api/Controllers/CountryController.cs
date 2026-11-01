using Microsoft.AspNetCore.Mvc;

namespace VeloceCRM.Api.Controllers
{
    public class CountryController : Controller
    {

        public CountryController()
        {

        }

        [HttpGet]
        [Route("api/country/get")]
        public IActionResult Get(long Id, string LicenseKey)
        {
            Repository.Repositories repositories = new Repository.Repositories(LicenseKey);
            return new OkObjectResult(repositories.CountryRepository.Get(Id));
        }
    }
}
