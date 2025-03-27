using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Base
{
    public class BaseAuditable
    {
        public bool Enabled { get; set; } = true;  // Indica si el registro está activo
        public string IUser { get; set; } = string.Empty;  // Usuario que creó el registro
        public DateTime IDate { get; set; } = DateTime.UtcNow;  // Fecha de creación
        public string IComments { get; set; } = string.Empty;  // Comentarios de creación
        public string? UUser { get; set; }  // Usuario que modificó el registro
        public DateTime? UDate { get; set; }  // Fecha de última modificación
        public string? UComments { get; set; }  // Comentarios de modificación
    }
}
