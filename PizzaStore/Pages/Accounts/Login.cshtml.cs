using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;
using System.Text.Json;

namespace PizzaStore.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly PRN221_As02Context dbContext;
        public LoginModel(PRN221_As02Context dbContext)
        {
            this.dbContext = dbContext;
        }
        [BindProperty]
        public PizzaStore.Models.Account Account { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
           
            var acc = await dbContext.Accounts
                        .FirstOrDefaultAsync(a => a.UserName.Equals(Account.UserName) && a.Password.Equals(Account.Password));

            if (acc == null)
            {
                ViewData["msg"] = "Email/ Password is wrong";
                return Page();
            }

            HttpContext.Session.SetString("CustSession", JsonSerializer.Serialize(acc));
            if (acc.Type == 1)
            {
                HttpContext.Session.Remove("CustSession");
                HttpContext.Session.SetString("IsAdmin", "Admin");
                return Redirect("/Admin/Products");
            }
            return RedirectToPage("/Customer/Index");
        }
    }
}
