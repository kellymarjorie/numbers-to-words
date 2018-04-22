using Microsoft.AspNetCore.Mvc;
using CucumberTechnicalTest.Models;
using System.Globalization;

namespace CucumberTechnicalTest.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new CurrencyConverterModel());
        }

        [HttpPost]
        public IActionResult Result(CurrencyConverterModel model)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            model.YourNameHere = textInfo.ToTitleCase(model.YourNameHere);
            model.ResultingText = model.ConvertNumberToString(model.NumberToConvert);

            return View(model);
        }
    }
}
