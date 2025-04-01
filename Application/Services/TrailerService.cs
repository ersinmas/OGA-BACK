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
            try
            {
                await _vehicleTrailerService.CheckExpiredAssignmentsAsync();
                var trailer = await _trailerRepository.GetAllAsync();
                var filteredTrailers = trailer.Where(t => t.Enabled); 
                return _mapper.Map<IEnumerable<TrailerDTO>>(filteredTrailers);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener los remolques", ex);
            }
        }

        public async Task<TrailerDTO?> GetTrailerByIdAsync(int id)
        {
            try
            {
                var trailer = await _trailerRepository.GetByIdAsync(id);
                return trailer != null ? _mapper.Map<TrailerDTO>(trailer) : null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener el remolque por ID", ex);
            }
        }

        public async Task AddTrailerAsync(TrailerDTO trailerDto)
        {
            try
            {
                var trailer = _mapper.Map<Trailer>(trailerDto);
                await _trailerRepository.AddAsync(trailer);
                await _trailerRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al agregar el remolque", ex);
            }
        }

        public async Task UpdateTrailerAsync(TrailerDTO trailerDto)
        {
            try
            {
                var trailer = _mapper.Map<Trailer>(trailerDto);
                _trailerRepository.Update(trailer);
                await _trailerRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al actualizar el remolque", ex);
            }
        }

        public async Task DeleteTrailerAsync(int id)
        {
            try
            {
                var trailer = await _trailerRepository.GetByIdAsync(id);
                if (trailer != null)
                {
                    _trailerRepository.Delete(trailer);
                    await _trailerRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar el remolque", ex);
            }
        }
    }
}
