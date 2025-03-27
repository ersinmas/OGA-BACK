using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IVehicleTrailerService
    {
        Task<IEnumerable<VehicleTrailerDTO>> GetAllVehicleTrailersAsync();
        Task<VehicleTrailerDTO?> GetVehicleTrailerByIdAsync(int id);
        Task AddVehicleTrailerAsync(VehicleTrailerDTO vehicleTrailerDto);
        Task UpdateVehicleTrailerAsync(VehicleTrailerDTO VehicleTrailerDTO);
        Task DeleteVehicleTrailerAsync(int id);
        Task  CheckExpiredAssignmentsAsync();


    }
}
