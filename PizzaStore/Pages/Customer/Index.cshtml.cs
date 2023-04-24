using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

namespace PizzaStore.Pages.Customer
{
    public class IndexModel : PageModel
    {
        private readonly PizzaStore.Models.PRN221_As02Context dbContext;

        public IndexModel(PizzaStore.Models.PRN221_As02Context dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<Product> Product { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (dbContext.Products != null)
            {
                Product = await dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier).ToListAsync();
            }
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
