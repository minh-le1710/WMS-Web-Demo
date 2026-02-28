using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Wms.Web.Models;
using Wms.Web.wwwroot.data;

namespace Wms.Web.Pages.Import;

public class IndexModel : PageModel
{
    private readonly WmsDbContext _db;
    private readonly IWebHostEnvironment _env;

    public IndexModel(WmsDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    public string? Message { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostSkusAsync()
    {
        var path = Path.Combine(_env.WebRootPath, "data", "skus.csv");
        if (!System.IO.File.Exists(path))
        {
            Message = $"Không tìm thấy file: {path}";
            return Page();
        }

        var lines = await System.IO.File.ReadAllLinesAsync(path);

        // Load 1 lần để upsert
        var existingMap = await _db.Skus.ToDictionaryAsync(x => x.Code, x => x);

        var added = 0;
        var updated = 0;
        var skipped = 0;

        foreach (var line in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line)) { skipped++; continue; }

            var p = line.Split(',');
            if (p.Length < 9) { skipped++; continue; }

            var code = p[0].Trim();
            if (string.IsNullOrWhiteSpace(code)) { skipped++; continue; }

            var name = p[1].Trim();
            var group = p[2].Trim();
            var unit = p[3].Trim();

            // Parse số an toàn
            decimal.TryParse(p[4].Trim(), out var cost);
            int.TryParse(p[5].Trim(), out var minStock);
            int.TryParse(p[6].Trim(), out var maxStock);
            int.TryParse(p[7].Trim(), out var reorderPoint);
            var isActive = bool.TryParse(p[8].Trim(), out var act) ? act : true;

            if (!existingMap.TryGetValue(code, out var sku))
            {
                sku = new Sku { Code = code };
                _db.Skus.Add(sku);
                existingMap[code] = sku;
                added++;
            }
            else
            {
                updated++;
            }

            sku.Name = name;
            sku.Group = string.IsNullOrWhiteSpace(group) ? null : group;
            sku.Unit = string.IsNullOrWhiteSpace(unit) ? null : unit;
            sku.Cost = cost;
            sku.MinStock = minStock;
            sku.MaxStock = maxStock;
            sku.ReorderPoint = reorderPoint;
            sku.IsActive = isActive;
        }

        await _db.SaveChangesAsync();
        Message = $"Import SKU xong: thêm {added}, cập nhật {updated}, bỏ qua {skipped}.";
        return Page();
    }

    public async Task<IActionResult> OnPostBinsAsync()
    {
        var path = Path.Combine(_env.WebRootPath, "data", "bins.csv");
        if (!System.IO.File.Exists(path))
        {
            Message = $"Không tìm thấy file: {path}";
            return Page();
        }

        var lines = await System.IO.File.ReadAllLinesAsync(path);
        var added = 0;

        foreach (var line in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split(',');
            if (parts.Length < 3) continue;

            var code = parts[0].Trim();
            if (_db.BinLocations.Any(x => x.Code == code)) continue;

            _db.BinLocations.Add(new BinLocation
            {
                Code = code,
                Description = parts[1].Trim(),
                IsActive = bool.TryParse(parts[2], out var act) ? act : true
            });
            added++;
        }

        await _db.SaveChangesAsync();
        Message = $"Import Bin xong: thêm mới {added} dòng.";
        return Page();
    }
    public async Task<IActionResult> OnPostContainersAsync()
    {
        var path = Path.Combine(_env.WebRootPath, "data", "containers.csv");
        if (!System.IO.File.Exists(path))
        {
            Message = $"Không tìm thấy file: {path}";
            return Page();
        }

        var lines = await System.IO.File.ReadAllLinesAsync(path);

        // Load tất cả containers hiện có 1 lần
        var existingMap = await _db.Containers
            .ToDictionaryAsync(x => x.ContainerNo, x => x);

        var added = 0;
        var updated = 0;
        var skipped = 0;

        foreach (var line in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line)) { skipped++; continue; }

            var p = line.Split(',');
            if (p.Length < 7) { skipped++; continue; }

            var no = p[0].Trim();
            if (string.IsNullOrWhiteSpace(no)) { skipped++; continue; }

            // Parse ngày an toàn
            if (!DateTime.TryParse(p[5].Trim(), out var inDate))
            {
                skipped++;
                continue;
            }

            var type = p[1].Trim();
            var size = p[2].Trim();
            var shipLine = p[3].Trim();
            var cargoStatus = p[4].Trim();
            var customs = p[6].Trim();

            if (!existingMap.TryGetValue(no, out var entity))
            {
                // Insert
                var newEntity = new ContainerRecord
                {
                    ContainerNo = no,
                    Type = type,
                    Size = size,
                    Line = shipLine,
                    CargoStatus = cargoStatus,
                    InDate = inDate,
                    CustomsStatus = customs
                };

                _db.Containers.Add(newEntity);
                existingMap[no] = newEntity; // chống trùng ngay trong file
                added++;
            }
            else
            {
                // Update
                entity.Type = type;
                entity.Size = size;
                entity.Line = shipLine;
                entity.CargoStatus = cargoStatus;
                entity.InDate = inDate;
                entity.CustomsStatus = customs;
                updated++;
            }
        }

        await _db.SaveChangesAsync();
        Message = $"Import Containers xong: thêm {added}, cập nhật {updated}, bỏ qua {skipped} dòng lỗi/trống.";
        return Page();
    }
}