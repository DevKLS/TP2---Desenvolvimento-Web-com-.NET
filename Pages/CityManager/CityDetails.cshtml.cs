
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TP2_RazorPages.Pages.CityManager
{
    public class CityDetailsModel : PageModel
    {
        public string CityName { get; set; } = string.Empty;

        public void OnGet(string cityName)
        {
            CityName = cityName;
        }
    }
}
