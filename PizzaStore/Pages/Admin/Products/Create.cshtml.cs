using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PizzaStore.Models;

namespace PizzaStore.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly PizzaStore.Models.PRN221_As02Context _context;
        public SelectList Categories { get; set; }
        public SelectList Suppliers { get; set; }

        public CreateModel(PizzaStore.Models.PRN221_As02Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            getCategories(_context);
            getSuppliers(_context);
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public void getCategories(PizzaStore.Models.PRN221_As02Context _context)
        {
            var categoriesQuery = from c in _context.Categories
                                   select c;

            Categories = new SelectList(categoriesQuery.AsNoTracking(),
                nameof(Category.CategoryId),
                nameof(Category.CategoryName));
        }

        public void getSuppliers(PRN221_As02Context context)
        {
            var suppliersQuery = from s in _context.Suppliers
                                   select s;

            Suppliers = new SelectList(suppliersQuery.AsNoTracking(),
                nameof(Supplier.SupplierId),
                nameof(Supplier.CompanyName));
        }
    }
}
