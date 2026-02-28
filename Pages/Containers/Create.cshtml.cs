using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wms.Web.Models;
using Wms.Web.wwwroot.data;

namespace Wms.Web.Pages.Containers
{
    public class CreateModel : PageModel
    {
        private readonly Wms.Web.wwwroot.data.WmsDbContext _context;

        public CreateModel(Wms.Web.wwwroot.data.WmsDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ContainerRecord ContainerRecord { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Containers.Add(ContainerRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
