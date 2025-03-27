using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;

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
            var vehicleType = _mapper.Map<VehicleType>(vehicleTypeDto);
            await _vehicleTypeRepository.AddAsync(vehicleType);
            await _vehicleTypeRepository.SaveChangesAsync();
        }

        public async Task DeleteVehicleTypeAsync(int id)
        {
            var vehicleType = await _vehicleTypeRepository.GetByIdAsync(id);
            if (vehicleType != null)
            {
                _vehicleTypeRepository.Delete(vehicleType);
                await _vehicleTypeRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<VehicleTypeDTO>> GetAllVehicleTypeAsync()
        {
            var vehicleType = await _vehicleTypeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<VehicleTypeDTO>>(vehicleType);
        }

        public async Task<VehicleTypeDTO?> GetVehicleTypeByIdAsync(int id)
        {
            var vehicleType = await _vehicleTypeRepository.GetByIdAsync(id);
            return vehicleType != null ? _mapper.Map<VehicleTypeDTO>(vehicleType) : null;
        }

        public async Task UpdateVehicleTypeAsync(VehicleTypeDTO VehicleTypeDTO)
        {
            var vehicleType = _mapper.Map<VehicleType>(VehicleTypeDTO);
            _vehicleTypeRepository.Update(vehicleType);
            await _vehicleTypeRepository.SaveChangesAsync();
        }
    }
}
