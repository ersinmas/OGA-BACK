using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using AutoMapper;
using Infrastructure.Repositories;
using System.Transactions;

namespace Application.Services
{
    public class VehicleTrailerService : IVehicleTrailerService
    {
        private readonly IVehicleTrailerRepository _vehicleTrailerRepository;

        private readonly IVehicleRepository _vehicleRepository;

        private readonly ITrailerRepository _trailerRepository;


        private readonly IMapper _mapper;

        public VehicleTrailerService(IVehicleTrailerRepository vehicleTrailerRepository, IMapper mapper, IVehicleRepository vehicleRepository, ITrailerRepository trailerRepository)
        {
            _vehicleTrailerRepository = vehicleTrailerRepository;
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _trailerRepository = trailerRepository;
        }

        public async Task<IEnumerable<VehicleTrailerDTO>> GetAllVehicleTrailersAsync()
        {
            var vehicleTrailers = await _vehicleTrailerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<VehicleTrailerDTO>>(vehicleTrailers);
        }

        public async Task AddVehicleTrailerAsync(VehicleTrailerDTO vehicleTrailerDto)
        {
            var vehicleTrailer = _mapper.Map<VehicleTrailer>(vehicleTrailerDto);
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleTrailer.VehicleId);
            var trailer = await _trailerRepository.GetByIdAsync(vehicleTrailer.TrailerId);
            vehicleTrailer.ValidateAssignment(vehicle, trailer);

            // Iniciamos la transacción
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (trailer != null)
                {
                    // Desactivamos el trailer
                    trailer.Enabled = false;
                    _trailerRepository.Update(trailer);
                    await _trailerRepository.SaveChangesAsync(); // Guardamos los cambios en el remolque

                    // Verificamos la actualización
                    var updatedTrailer = await _trailerRepository.GetByIdAsync(trailer.TrailerId);
                    Console.WriteLine($"Trailer {updatedTrailer.TrailerId} updated with Enabled={updatedTrailer.Enabled}");
                }

                // Ahora agregamos la relación de trailer y vehículo
                await _vehicleTrailerRepository.AddAsync(vehicleTrailer);
                await _vehicleTrailerRepository.SaveChangesAsync(); // Guardamos la relación

                 scope.Complete();
            }
        }


        public Task<VehicleTrailerDTO?> GetVehicleTrailerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<VehicleTrailer?> GetVehicleTrailerByIdAsync(VehicleTrailerDTO VehicleTrailerDTO)
        {
          var relaciones =  await _vehicleTrailerRepository.GetAllAsync();
          var resultado = relaciones.FirstOrDefault(rel =>
              rel.VehicleId == VehicleTrailerDTO.VehicleId &&
              rel.TrailerId == VehicleTrailerDTO.TrailerId &&
              rel.BegDate >= VehicleTrailerDTO.BegDate &&
              rel.EndDate <= VehicleTrailerDTO.EndDate);

          return resultado;

        }


        public async Task UpdateVehicleTrailerAsync(VehicleTrailerDTO VehicleTrailerDTO)
        {
            //Utilizaremos este put solo para finalizar la asignacion (se cambiara la fecha fin por la actual)

            var vehicleTrailerNew = await this.GetVehicleTrailerByIdAsync(VehicleTrailerDTO);

            if (vehicleTrailerNew == null)
            {
                throw new Exception("No se encontró la relación de VehicleTrailer para actualizar.");

            }

            await this.DeleteVehicleTrailerAsync(vehicleTrailerNew); //eliminamos el actual

            vehicleTrailerNew.EndAsig(); //cambiamos la fecha de fin por la de ahora
            await _vehicleTrailerRepository.AddAsync(vehicleTrailerNew); // creamos de nuevo la relacion 
            await _vehicleTrailerRepository.SaveChangesAsync();
        }

        public async Task DeleteVehicleTrailerAsync(VehicleTrailer vehicleTrailer)
        {
            if (vehicleTrailer != null)
            {
                _vehicleTrailerRepository.Delete(vehicleTrailer);
                await _vehicleTrailerRepository.SaveChangesAsync();
            }
        }

        public async Task CheckExpiredAssignmentsAsync()
        {
            var expiredAssignments = await _vehicleTrailerRepository.GetAllAsync();

            var expireUp = expiredAssignments.Where(vt => vt.EndDate <= DateTime.UtcNow  && vt.Enabled);


            foreach (var assignment in expireUp)
            {
                assignment.Enabled = false;
                _vehicleTrailerRepository.Update(assignment);
                var trailer = await _trailerRepository.GetByIdAsync(assignment.TrailerId);
                if (trailer != null)
                {
                    trailer.Enabled = true;
                     _trailerRepository.Update(trailer);
                }
            }

            await _vehicleTrailerRepository.SaveChangesAsync();
            await _trailerRepository.SaveChangesAsync();

        }

        public Task DeleteVehicleTrailerAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}