using Bogus;
using DataTableServerSide.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServerSideTable.Demo.NET8.Models;
using System.Diagnostics;

namespace ServerSideTable.Demo.NET8.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetPersons()
        {
            var personList = Persons();
            var dataTableHelper = new DataTableHelper<PersonModel>(personList, Request);
            var result = dataTableHelper.GetDataTableResult();
            return Json(result);
        }

        public List<PersonModel> Persons()
        {
            List<PersonModel> persons = new();

            var faker = new Faker();

            for (int i = 0; i < 10000; i++)
            {
                PersonModel person = new()
                {
                    Id = i + 1,
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    Phone = faker.Phone.PhoneNumberFormat(),
                    Mail = faker.Internet.Email(),
                    Address = faker.Address.FullAddress(),
                    Age = faker.Random.Number(18, 80)

                };
                persons.Add(person);
            }

            return persons;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
