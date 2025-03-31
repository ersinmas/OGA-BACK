using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class VehicleTypeService : IVehicleTypeService
    {

        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly IMapper _mapper;
        public VehicleTypeService(IVehicleTypeRepository vehicleTypeRepository, IMapper mapper)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
            _mapper = mapper;
        }

        public async Task AddVehicleTypeAsync(VehicleTypeDTO vehicleTypeDto)
        {
            try
            {
                var vehicleType = _mapper.Map<VehicleType>(vehicleTypeDto);
                await _vehicleTypeRepository.AddAsync(vehicleType);
                await _vehicleTypeRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar VehicleType: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteVehicleTypeAsync(int id)
        {
            try
            {
                var vehicleType = await _vehicleTypeRepository.GetByIdAsync(id);
                if (vehicleType != null)
                {
                    _vehicleTypeRepository.Delete(vehicleType);
                    await _vehicleTypeRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar VehicleType: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<VehicleTypeDTO>> GetAllVehicleTypeAsync()
        {
            try
            {
                var vehicleType = await _vehicleTypeRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<VehicleTypeDTO>>(vehicleType);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los VehicleTypes: {ex.Message}");
                throw;
            }
        }

        public async Task<VehicleTypeDTO?> GetVehicleTypeByIdAsync(int id)
        {
            try
            {
                var vehicleType = await _vehicleTypeRepository.GetByIdAsync(id);
                return vehicleType != null ? _mapper.Map<VehicleTypeDTO>(vehicleType) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener VehicleType por ID: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateVehicleTypeAsync(VehicleTypeDTO VehicleTypeDTO)
        {
            try
            {
                var vehicleType = _mapper.Map<VehicleType>(VehicleTypeDTO);
                _vehicleTypeRepository.Update(vehicleType);
                await _vehicleTypeRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar VehicleType: {ex.Message}");
                throw;
            }
        }
    }
}
