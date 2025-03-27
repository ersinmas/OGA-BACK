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

namespace Application.Services
{
    public class TrailerService : ITrailerService
    {
        private readonly ITrailerRepository _trailerRepository;
        private readonly IVehicleTrailerService _vehicleTrailerService;

        private readonly IMapper _mapper;

        public TrailerService(ITrailerRepository trailerRepository, IMapper mapper, IVehicleTrailerService vehicleTrailerService)
        {
            _trailerRepository = trailerRepository;
            _mapper = mapper;
            _vehicleTrailerService = vehicleTrailerService;
        }

        public async Task<IEnumerable<TrailerDTO>> GetAllTrailersAsync()
        {
            await _vehicleTrailerService.CheckExpiredAssignmentsAsync();
            var trailer = await _trailerRepository.GetAllAsync();
            var filteredTrailers = trailer.Where(t => t.Enabled); //Filtrar solo los que tienen Enabled = true
            return _mapper.Map<IEnumerable<TrailerDTO>>(filteredTrailers);
        }

        public async Task<TrailerDTO?> GetTrailerByIdAsync(int id)
        {

            var trailer = await _trailerRepository.GetByIdAsync(id);
            return trailer != null ? _mapper.Map<TrailerDTO>(trailer) : null;
        }

        public async Task AddTrailerAsync(TrailerDTO trailerDto)
        {
            var trailer = _mapper.Map<Trailer>(trailerDto);
            await _trailerRepository.AddAsync(trailer);
            await _trailerRepository.SaveChangesAsync();
        }

        public async Task UpdateTrailerAsync(TrailerDTO trailerDto)
        {
            var trailer = _mapper.Map<Trailer>(trailerDto);
            _trailerRepository.Update(trailer);
            await _trailerRepository.SaveChangesAsync();
        }

        public async Task DeleteTrailerAsync(int id)
        {
            var trailer = await _trailerRepository.GetByIdAsync(id);
            if (trailer != null)
            {
                _trailerRepository.Delete(trailer);
                await _trailerRepository.SaveChangesAsync();
            }
        }


    }
}
