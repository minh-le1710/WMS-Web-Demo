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
    public class IndexModel : PageModel
    {
        private readonly WmsDbContext _context;

        public IndexModel(WmsDbContext context)
        {
            _context = context;
        }

        public IList<Sku> Sku { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Sku = await _context.Skus.ToListAsync();
        }
    }
}
