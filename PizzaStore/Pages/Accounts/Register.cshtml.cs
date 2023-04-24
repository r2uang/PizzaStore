using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;
using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Pages.Accounts
{
    public class RegisterModel : PageModel
    {
        private readonly PRN221_As02Context dbContext;
        public RegisterModel(PRN221_As02Context dbContext)
        {
            this.dbContext = dbContext;
        }
        [BindProperty]
        public PizzaStore.Models.Customer Customer { get; set; }

        [BindProperty]
        public PizzaStore.Models.Account Account { get; set; }

        [BindProperty, Required(ErrorMessage = "Re-password is required")]
        public string RePassword { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {

            {
                var accId = await dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == Account.AccountId);
                if (accId != null)
                {
                    ViewData["msg"] = "This id is exist";
                    return Page();
                }
                var accUserName = await dbContext.Accounts.FirstOrDefaultAsync(a => a.UserName.Equals(Account.UserName));
                if (accUserName != null)
                {
                    ViewData["msg"] = "This username is exist";
                    return Page();
                }
                if (RePassword != Account.Password)
                {
                    ViewData["msg-repassword"] = "Re-password not match";
                    return Page();
                }

                var newAcc = new Models.Account()
                {
                    AccountId = Account.AccountId,
                    UserName = Account.UserName,
                    Password = Account.Password,
                    FullName = Account.FullName,
                    Type = 2,
                };
                var customer = new Models.Customer()
                {
                    CustomerId = Account.AccountId,
                    Password = Account.Password,
                    ContactName = Customer.ContactName,
                    Address = Customer.Address,
                    Phone = Customer.Phone
                };
                await dbContext.Customers.AddAsync(customer);
                await dbContext.Accounts.AddAsync(newAcc);
                await dbContext.SaveChangesAsync();
                return RedirectToPage("/Index");
            }
        }
    }
}
