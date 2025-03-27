using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class VehicleTrailerDTO
    {
        public int VehicleId { get; set; }
        public int TrailerId { get; set; }
        public DateTime BegDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
