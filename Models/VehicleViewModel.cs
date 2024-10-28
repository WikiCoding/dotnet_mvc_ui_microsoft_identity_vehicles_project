namespace VehiclesWebApp.Models;

public class VehicleViewModel
{
    public Guid VehicleId { get; set; }
    public string VehicleMake { get; set; } = string.Empty;
    public string VehicleModel { get; set; } = string.Empty;
    public string LicensePlate { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
}
