using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class VehicleDTO
    {
        public int VehicleId { get; set; }
        public int VehicleTypeId { get; set; }
        public int RegNumber { get; set; }
        public DateTime RegDate { get; set; }
    }
}
