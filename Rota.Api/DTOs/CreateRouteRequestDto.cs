using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Rota.Api.Dtos
{
    public class CreateRouteRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Optimization { get; set; } = "distance";

        public double TotalLoadWeightKg { get; set; }
        public double TotalLoadVolumeM3 { get; set; }

        public int? VehicleId { get; set; }

        public List<WaypointDto> Waypoints { get; set; } = new();
    }
}
