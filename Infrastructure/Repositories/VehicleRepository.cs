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
    public class VehicleRepository : GenericRepository<Vehicles>, IVehicleRepository
    {
        public VehicleRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Vehicles>> GetVehiclesWithTypeAsync()
        {
            return await _dbSet.Include(v => v.VehicleType).ToListAsync();
        }
    }
}
