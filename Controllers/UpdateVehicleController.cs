using Microsoft.AspNetCore.Mvc;
using VehiclesWebApp.Models;
using VehiclesWebApp.Service;

namespace VehiclesWebApp.Controllers;

public class UpdateVehicleController : Controller
{
    private readonly VehicleService _vehicleService;
    private readonly ILogger<UpdateVehicleController> _logger;

    public UpdateVehicleController(VehicleService vehicleService, ILogger<UpdateVehicleController> logger)
    {
        _vehicleService = vehicleService;
        _logger = logger;
    }

    // GET
    public IActionResult Index(string vehicleId, string vehicleModel, string vehicleMake, string licensePlate)
    {
        if (User.Identity is null)
        {
            return RedirectToAction("ErrorUnauthorized", "Login");
        }

        var owner = User.Identity.Name;
        
        var vehicle = new VehicleViewModel
        {
            VehicleId = new Guid(vehicleId),
            VehicleMake = vehicleMake,
            VehicleModel = vehicleModel,
            LicensePlate = licensePlate,
            Owner = owner!
        };
        
        return View(vehicle);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(VehicleViewModel vehicleViewModel)
    {
        if (User.Identity.Name is null)
        {
            return RedirectToAction("ErrorUnauthorized", "Login");
        }

        var owner = User.Identity.Name;

        try
        {
            await _vehicleService.UpdateVehicle(vehicleViewModel.VehicleMake, vehicleViewModel.VehicleModel,
                vehicleViewModel.LicensePlate, owner);
            
            return RedirectToAction("Index", "Dashboard");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error updating: {message}", ex.Message);
            return RedirectToAction("Error", "Home");
        }
    }
}