using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehiclesWebApp.Models;
using VehiclesWebApp.Service;

namespace VehiclesWebApp.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly VehicleService _vehicleService;

    public DashboardController(VehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public async Task<IActionResult> Index()
    {
        string username = User.Identity.Name;
        
        if (username is null)
        {
            return RedirectToAction("ErrorUnauthorized", "Login");
        }

        var vehicles = await _vehicleService.FindAllByUsername(username);

        var vehiclesViewModel = new List<VehicleViewModel>();

        foreach (var vehicle in vehicles)
        {
            var vehicleViewModel = new VehicleViewModel
            {
                VehicleId = vehicle.VehicleId,
                VehicleMake = vehicle.VehicleMake,
                VehicleModel = vehicle.VehicleModel,
                LicensePlate = vehicle.LicensePlate
            };

            vehiclesViewModel.Add(vehicleViewModel);
        }

        return View(vehiclesViewModel);
    }

    public IActionResult RedirectToAddPage()
    {
        return RedirectToAction("Index", "AddVehicle");
    }
}
