using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDTO>> GetAllVehiclesAsync();
        Task<VehicleDTO?> GetVehicleByIdAsync(int id);
        Task AddVehicleAsync(VehicleDTO vehicleDto);
        Task UpdateVehicleAsync(VehicleDTO vehicleDto);
        Task DeleteVehicleAsync(int id);
    }
}
