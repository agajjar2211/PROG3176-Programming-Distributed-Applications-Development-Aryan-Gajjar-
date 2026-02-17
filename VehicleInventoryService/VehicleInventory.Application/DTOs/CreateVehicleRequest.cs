using System.ComponentModel.DataAnnotations;

namespace VehicleInventory.Application.DTOs;

public class CreateVehicleRequest
{
    [Required, StringLength(20)]
    public string VehicleCode { get; set; } = default!;

    [Required, StringLength(50)]
    public string LocationId { get; set; } = default!;

    [Required, StringLength(50)]
    public string VehicleType { get; set; } = default!;
}