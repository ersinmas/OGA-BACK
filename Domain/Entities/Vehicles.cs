using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;

namespace Domain.Entities
{
    public class Vehicles:BaseAuditable
    {
        [Key]
        public int VehicleId { get; set; }  // Identificador único del vehículo
        public int VehicleTypeId { get; set; }  // Tipo de vehículo
        public int RegNumber { get; set; }  // Número de registro
        public DateTime RegDate { get; set; }  // Fecha de registro del vehículo

        public VehicleType VehicleType { get; set; } = null;
        public ICollection<VehicleTrailer> VehicleTrailer { get; set; } = new List<VehicleTrailer>();

    }

}
