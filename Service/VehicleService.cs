using Microsoft.EntityFrameworkCore;
using VehiclesWebApp.Infrastructure;

namespace VehiclesWebApp.Service;

public class VehicleService
{
    private readonly AppDbContext _appDbContext;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly ILogger<VehicleService> _logger;

    public VehicleService(AppDbContext appDbContext, ILogger<VehicleService> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<VehicleDataModel>> FindAllByUsername(string username)
    {
        _logger.LogInformation("Getting vehicles from {username}", username);
        var vehicles = await _appDbContext.Vehicles.Where(v => v.Owner == username).ToListAsync();

        return vehicles;
    }

    public async Task AddVehicle(string vehicleMake, string vehicleModel, string licensePlate, string username)
    {
        var vehicleDm = new VehicleDataModel 
        { 
            VehicleId = new Guid(), 
            VehicleMake = vehicleMake, 
            VehicleModel = vehicleModel, 
            LicensePlate = licensePlate,
            Owner = username
        };

        try
        {
            await _semaphore.WaitAsync();

            _appDbContext.Add(vehicleDm);

            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Saved successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error saving {message}", ex.Message);
            throw;
        }
        finally
        {
            _logger.LogDebug("Releasing lock");
            _semaphore.Release();
        }
    }

    public async Task UpdateVehicle(string vehicleMake, string vehicleModel, string licensePlate, string username)
    {
        try
        {
            await _semaphore.WaitAsync();

            await _appDbContext.Vehicles.Where(v => v.Owner == username)
                .ExecuteUpdateAsync(v =>
                    v.SetProperty(x => x.VehicleMake, vehicleMake)
                        .SetProperty(x => x.VehicleModel, vehicleModel)
                        .SetProperty(x => x.LicensePlate, licensePlate));

            _logger.LogInformation("Updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error updating: {message}", ex.Message);
            throw;
        }
        finally
        {
            _logger.LogDebug("Releasing lock");
            _semaphore.Release();
        }
    }

    public async Task DeleteVehicle(string vehicleId)
    {
        try
        {
            await _semaphore.WaitAsync();
            await _appDbContext.Vehicles.Where(v => v.VehicleId == new Guid(vehicleId)).ExecuteDeleteAsync();
            
            _logger.LogInformation("Deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error updating: {message}", ex.Message);
            throw;
        }
        finally
        {
            _logger.LogDebug("Releasing lock");
            _semaphore.Release();
        }
    }
}
