using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IVehicleRepository: IGenericRepository<Vehicles>
    { 
        Task<IEnumerable<Vehicles>> GetVehiclesWithTypeAsync();
    }
}
