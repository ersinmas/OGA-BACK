using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;
using Microsoft.VisualBasic;

namespace Domain.Entities
{
    public class VehicleTrailer : BaseAuditable
    {
        public int VehicleId { get; set; }
        public int TrailerId { get; set; }
        public DateTime BegDate { get; set; }
        public DateTime? EndDate { get; set; } = null;
        public Trailer Trailer { get; set; } = null;
        public Vehicles Vehicles { get; set; }

        public void ValidateAssignment(Vehicles vehicle, Trailer trailer)
        {

            if (vehicle.VehicleTypeId == 1 && TrailerId != null)
            {
                throw new InvalidOperationException("Las motos no pueden tener remolques.");
            }
            if (vehicle.VehicleTypeId == 2 && trailer.MaxWeight >= 1500)
            {
                throw new InvalidOperationException("Los coches no pueden tener remolques de mas de 1500kg");
            }
            if (vehicle.VehicleTypeId == 3 && trailer.MaxWeight <= 1500)
            {
                throw new InvalidOperationException("Los camiones no pueden tener remolques de menos de 1500kg");
            }


        }

        public void EndAsig()
        {
            this.EndDate = DateTime.Now;

        }

    }
}
