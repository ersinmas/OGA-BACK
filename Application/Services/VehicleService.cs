using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var vehicles = await _vehicleRepository.GetVehiclesWithTypeAsync();
            return _mapper.Map<IEnumerable<VehicleDTO>>(vehicles);
        }

        public async Task<VehicleDTO?> GetVehicleByIdAsync(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            return vehicle != null ? _mapper.Map<VehicleDTO>(vehicle) : null;
        }

        public async Task AddVehicleAsync(VehicleDTO vehicleDto)
        {
            var vehicle = _mapper.Map<Vehicles>(vehicleDto);
            await _vehicleRepository.AddAsync(vehicle);
            await _vehicleRepository.SaveChangesAsync();
        }

        public async Task UpdateVehicleAsync(VehicleDTO vehicleDto)
        {
            var vehicle = _mapper.Map<Vehicles>(vehicleDto);
            _vehicleRepository.Update(vehicle);
            await _vehicleRepository.SaveChangesAsync();
        }

        public async Task DeleteVehicleAsync(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle != null)
            {
                _vehicleRepository.Delete(vehicle);
                await _vehicleRepository.SaveChangesAsync();
            }
        }
    }
}
