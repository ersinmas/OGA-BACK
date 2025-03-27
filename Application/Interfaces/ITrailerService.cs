using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface ITrailerService
    {
        Task<IEnumerable<TrailerDTO>> GetAllTrailersAsync();
        Task<TrailerDTO?> GetTrailerByIdAsync(int id);
        Task AddTrailerAsync(TrailerDTO trailerDto);
        Task UpdateTrailerAsync(TrailerDTO trailerDto);
        Task DeleteTrailerAsync(int id);
    }
}
