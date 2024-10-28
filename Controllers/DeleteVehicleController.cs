using Microsoft.AspNetCore.Mvc;
using VehiclesWebApp.Service;

namespace VehiclesWebApp.Controllers;

public class DeleteVehicleController : Controller
{
    private readonly VehicleService _vehicleService;

    public DeleteVehicleController(VehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    // GET
    public async Task<IActionResult> Index(string vehicleId)
    {
        await _vehicleService.DeleteVehicle(vehicleId);
        return RedirectToAction("Index", "Dashboard");
    }
}