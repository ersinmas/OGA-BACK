using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;

namespace Domain.Entities
{
    public class Trailer:BaseAuditable
    {
        public int TrailerId { get; set; }
        public int RegNumber { get; set; }
        public DateTime RegDate { get; set; }
        public int MaxWeight { get; set; }

        public ICollection<VehicleTrailer> VehicleTrailer { get; set; } = new List<VehicleTrailer>();
    }
}
