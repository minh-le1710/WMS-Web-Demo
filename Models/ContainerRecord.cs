using System.ComponentModel.DataAnnotations;

namespace Wms.Web.Models;

public class ContainerRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, StringLength(15)]
    public string ContainerNo { get; set; } = default!;   // MAEU8172635

    [Required, StringLength(20)]
    public string Type { get; set; } = default!;          // Dry, Reefer...

    [Required, StringLength(10)]
    public string Size { get; set; } = default!;          // 20'DC, 40'HC...

    [Required, StringLength(60)]
    public string Line { get; set; } = default!;          // Maersk, MSC...

    [Required, StringLength(10)]
    public string CargoStatus { get; set; } = default!;   // Full/Empty

    public DateTime InDate { get; set; }                  // Ngày vào

    [StringLength(60)]
    public string? CustomsStatus { get; set; }            // Đã thông quan...
}