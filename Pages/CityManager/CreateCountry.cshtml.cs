using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TP2_RazorPages.Models;

namespace TP2_RazorPages.Pages.CityManager
{
    public class CreateCountryModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = new();

        public Country? RegisteredCountry { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O nome do país é obrigatório.")]
            public string CountryName { get; set; } = string.Empty;

            [Required(ErrorMessage = "O código do país é obrigatório.")]
            [StringLength(2, MinimumLength = 2,
                ErrorMessage = "O código deve possuir exatamente 2 caracteres.")]
            public string CountryCode { get; set; } = string.Empty;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            if (char.ToUpperInvariant(Input.CountryName[0]) !=
                char.ToUpperInvariant(Input.CountryCode[0]))
            {
                ModelState.AddModelError(
                    "Input.CountryCode",
                    "A primeira letra do código deve ser igual à primeira letra do país.");

                return;
            }

            RegisteredCountry = new Country
            {
                CountryName = Input.CountryName,
                CountryCode = Input.CountryCode
            };
        }
    }
}