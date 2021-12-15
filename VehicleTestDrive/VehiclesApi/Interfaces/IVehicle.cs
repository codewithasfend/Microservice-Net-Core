using VehiclesApi.Models;

namespace VehiclesApi.Interfaces
{
    public interface IVehicle
    {
        Task<List<Vehicle>> GetAllVehicles();
        Task<Vehicle> GetVehicleById(int id);   
        Task AddVehicle(Vehicle vehicle);
        Task UpdateVehicle(int id , Vehicle vehicle);
        Task DeleteVehicle(int id);
    }
}
