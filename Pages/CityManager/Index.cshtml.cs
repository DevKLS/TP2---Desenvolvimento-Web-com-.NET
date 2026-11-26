
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TP2_RazorPages.Pages.CityManager
{
    public class IndexModel : PageModel
    {
        public List<string> Cities { get; set; } = new()
        {
            "Rio",
            "São Paulo",
            "Brasília"
        };

        public void OnGet()
        {
        }
    }
}
