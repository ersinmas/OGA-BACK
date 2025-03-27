using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IVehicleTypeService
    {
        Task<IEnumerable<VehicleTypeDTO>> GetAllVehicleTypeAsync();
        Task<VehicleTypeDTO?> GetVehicleTypeByIdAsync(int id);
        Task AddVehicleTypeAsync(VehicleTypeDTO VehicleTypeDTO);
        Task UpdateVehicleTypeAsync(VehicleTypeDTO VehicleTypeDTO);
        Task DeleteVehicleTypeAsync(int id);
    }
}
