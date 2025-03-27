using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;

namespace Domain.Entities
{
    public class VehicleType:BaseAuditable
    {
        public int VehicleTypeId { get; set; }
        public string Name { get; set; }

        public ICollection<Vehicles> Vehicles { get; set; } = new List<Vehicles>();
    }
}
