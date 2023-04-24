using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PizzaStore.Pages.Accounts
{
    public class LogOutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            HttpContext.Session.Clear();
            return Redirect("/Accounts/Login");
        }
    }
}
