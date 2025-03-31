using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using AutoMapper;

namespace Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public VehicleService(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VehicleDTO>> GetAllVehiclesAsync()
        {
            try
            {
                var vehicles = await _vehicleRepository.GetVehiclesWithTypeAsync();
                return _mapper.Map<IEnumerable<VehicleDTO>>(vehicles);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener los vehículos", ex);
            }
        }

        public async Task<VehicleDTO?> GetVehicleByIdAsync(int id)
        {
            try
            {
                var vehicle = await _vehicleRepository.GetByIdAsync(id);
                return vehicle != null ? _mapper.Map<VehicleDTO>(vehicle) : null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener el vehículo por ID", ex);
            }
        }

        public async Task AddVehicleAsync(VehicleDTO vehicleDto)
        {
            try
            {
                var vehicle = _mapper.Map<Vehicles>(vehicleDto);
                await _vehicleRepository.AddAsync(vehicle);
                await _vehicleRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al agregar el vehículo", ex);
            }
        }

        public async Task UpdateVehicleAsync(VehicleDTO vehicleDto)
        {
            try
            {
                var vehicle = _mapper.Map<Vehicles>(vehicleDto);
                _vehicleRepository.Update(vehicle);
                await _vehicleRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al actualizar el vehículo", ex);
            }
        }

        public async Task DeleteVehicleAsync(int id)
        {
            try
            {
                var vehicle = await _vehicleRepository.GetByIdAsync(id);
                if (vehicle != null)
                {
                    _vehicleRepository.Delete(vehicle);
                    await _vehicleRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar el vehículo", ex);
            }
        }
    }
}
