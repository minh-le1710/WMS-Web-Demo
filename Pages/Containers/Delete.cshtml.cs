using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Wms.Web.Models;
using Wms.Web.wwwroot.data;

namespace Wms.Web.Pages.Containers
{
    public class DeleteModel : PageModel
    {
        private readonly Wms.Web.wwwroot.data.WmsDbContext _context;

        public DeleteModel(Wms.Web.wwwroot.data.WmsDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ContainerRecord ContainerRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var containerrecord = await _context.Containers.FirstOrDefaultAsync(m => m.Id == id);

            if (containerrecord == null)
            {
                return NotFound();
            }
            else
            {
                ContainerRecord = containerrecord;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var containerrecord = await _context.Containers.FindAsync(id);
            if (containerrecord != null)
            {
                ContainerRecord = containerrecord;
                _context.Containers.Remove(ContainerRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
