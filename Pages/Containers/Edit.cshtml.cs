using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wms.Web.Models;
using Wms.Web.wwwroot.data;

namespace Wms.Web.Pages.Containers
{
    public class EditModel : PageModel
    {
        private readonly Wms.Web.wwwroot.data.WmsDbContext _context;

        public EditModel(Wms.Web.wwwroot.data.WmsDbContext context)
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

            var containerrecord =  await _context.Containers.FirstOrDefaultAsync(m => m.Id == id);
            if (containerrecord == null)
            {
                return NotFound();
            }
            ContainerRecord = containerrecord;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ContainerRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContainerRecordExists(ContainerRecord.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ContainerRecordExists(Guid id)
        {
            return _context.Containers.Any(e => e.Id == id);
        }
    }
}
