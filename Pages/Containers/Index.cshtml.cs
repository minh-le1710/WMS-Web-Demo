using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Wms.Web.Models;

namespace Wms.Web.Pages.Containers;

public class IndexModel : PageModel
{
    private readonly Wms.Web.wwwroot.data.WmsDbContext _context;

    public IndexModel(Wms.Web.wwwroot.data.WmsDbContext context)
    {
        _context = context;
    }

    public IList<ContainerRecord> ContainerRecord { get; set; } = new List<ContainerRecord>();

    public async Task OnGetAsync()
    {
        ContainerRecord = await _context.Containers
            .AsNoTracking()
            .OrderByDescending(x => x.InDate)
            .ToListAsync();
    }
}