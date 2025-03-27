using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TrailerDTO
    {
        public int TrailerId { get; set; }
        public int RegNumber { get; set; }
        public DateTime RegDate { get; set; }
        public int MaxWeight { get; set; }
    }
}
