using System.ComponentModel.DataAnnotations;

namespace Wms.Web.Models;

public class Sku
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, StringLength(50)]
    public string Code { get; set; } = default!;   // Mã hàng

    [Required, StringLength(200)]
    public string Name { get; set; } = default!;   // Mô tả

    [StringLength(50)]
    public string? Group { get; set; }             // Nhóm

    [StringLength(30)]
    public string? Unit { get; set; }              // ĐVT

    [Range(0, 999999999)]
    public decimal Cost { get; set; }              // Giá vốn

    public int MinStock { get; set; }              // Tồn min
    public int MaxStock { get; set; }              // Tồn max
    public int ReorderPoint { get; set; }          // Điểm đặt lại

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}