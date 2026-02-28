using System.ComponentModel.DataAnnotations;

namespace Wms.Web.Models;

public class BinLocation
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, StringLength(50)]
    public string Code { get; set; } = default!;

    [StringLength(200)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}