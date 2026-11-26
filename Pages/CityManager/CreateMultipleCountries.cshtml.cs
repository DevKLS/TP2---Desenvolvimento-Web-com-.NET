
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TP2_RazorPages.Models;

namespace TP2_RazorPages.Pages.CityManager
{
    public class CreateMultipleCountriesModel : PageModel
    {
        [BindProperty]
        public List<InputModel> Countries { get; set; } = new();

        public List<Country> RegisteredCountries { get; set; } = new();

        public class InputModel
        {
            public string CountryName { get; set; } = string.Empty;

            public string CountryCode { get; set; } = string.Empty;
        }

        public void OnGet()
        {
            Countries = Enumerable.Range(0, 5)
                .Select(i => new InputModel())
                .ToList();
        }

        public void OnPost()
        {
            if (Countries.Count != 5)
            {
                ModelState.AddModelError(string.Empty,
                    "É necessário enviar exatamente 5 países.");

                return;
            }

            RegisteredCountries = Countries
                .Select(country => new Country
                {
                    CountryName = country.CountryName,
                    CountryCode = country.CountryCode
                })
                .ToList();
        }
    }
}
