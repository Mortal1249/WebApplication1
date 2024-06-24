using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;

        public LoginModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (_userService.Authenticate(Username, Password))
            {
                HttpContext.Session.SetString("Username", Username);
                HttpContext.Session.SetString("IsAuthenticated", "true");
                HttpContext.Session.SetString("IsAdmin", _userService.IsAdmin(Username).ToString());
                return RedirectToPage("/Items");
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }
        }
    }
}
