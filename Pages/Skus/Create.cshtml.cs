using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wms.Web.Models;
using Wms.Web.wwwroot.data;

namespace Wms.Web.Pages.Skus
{
    public class CreateModel : PageModel
    {
        private readonly WmsDbContext _context;

        public CreateModel(WmsDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Sku Sku { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Skus.Add(Sku);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
