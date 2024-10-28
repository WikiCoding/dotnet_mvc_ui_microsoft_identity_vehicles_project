using Microsoft.AspNetCore.Mvc;
using VehiclesWebApp.Models;
using VehiclesWebApp.Service;

namespace VehiclesWebApp.Controllers
{
    public class AddVehicleController : Controller
    {
        private readonly VehicleService _vehicleService;

        public AddVehicleController(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle(VehicleViewModel vehicleViewModel)
        {
            if (User.Identity is null)
            {
                return RedirectToAction("ErrorUnauthorized", "Login");
            }

            var owner = User.Identity.Name;

            try
            {
                await _vehicleService.AddVehicle(vehicleViewModel.VehicleMake, vehicleViewModel.VehicleModel, vehicleViewModel.LicensePlate, owner!);

                return RedirectToAction("Index", "Dashboard");
            } catch (Exception ex) 
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
