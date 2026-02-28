using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Wms.Web.Models;
using Wms.Web.wwwroot.data;

namespace Wms.Web.Pages.Skus
{
    public class DetailsModel : PageModel
    {
        private readonly WmsDbContext _context;

        public DetailsModel(WmsDbContext context)
        {
            _context = context;
        }

        public Sku Sku { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sku = await _context.Skus.FirstOrDefaultAsync(m => m.Id == id);
            if (sku == null)
            {
                return NotFound();
            }
            else
            {
                Sku = sku;
            }
            return Page();
        }
    }
}
