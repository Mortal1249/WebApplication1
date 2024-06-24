using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace WebApplication1.Pages
{
    public class CheckoutModel : PageModel
    {

        public IActionResult OnGet()
        {
            return Page();
        }


        public async Task<IActionResult> OnPostProcessPaymentAsync()
        {
            return new OkResult();
        }
    }
}
