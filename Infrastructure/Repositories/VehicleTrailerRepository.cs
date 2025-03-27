using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class VehicleTrailerRepository : GenericRepository<VehicleTrailer>, IVehicleTrailerRepository
    {
        public VehicleTrailerRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<VehicleTrailer>> GetActiveVehicleTrailersAsync()
        {
            return await _dbSet.Where(vt => vt.EndDate == null).ToListAsync();
        }
    }
}
