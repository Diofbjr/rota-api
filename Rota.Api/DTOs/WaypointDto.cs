using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rota.Api.Dtos
{
    public class WaypointDto
    {
        public int Order { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
