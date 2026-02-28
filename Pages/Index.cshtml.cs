using Microsoft.AspNetCore.Mvc.RazorPages;
using Wms.Web.wwwroot.data;

namespace Wms.Web.Pages;

public class IndexModel : PageModel
{
    private readonly WmsDbContext _db;
    public IndexModel(WmsDbContext db) => _db = db;

    public int TotalSkus { get; set; }
    public int TotalBins { get; set; }

    // demo placeholder (khi làm StockMovement sẽ tính thật)
    public int TotalOnHand { get; set; }
    public int TodayMovements { get; set; }

    public void OnGet()
    {
        TotalSkus = _db.Skus.Count();
        TotalBins = _db.BinLocations.Count();
        TotalOnHand = 0;
        TodayMovements = 0;
    }
}