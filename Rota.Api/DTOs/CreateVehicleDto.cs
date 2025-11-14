using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Rota.Api.Dtos
{
    public class CreateVehicleDto
    {
        public string Name { get; set; } = string.Empty;

        public double MaxWeightKg { get; set; }
        public double MaxVolumeM3 { get; set; }
        public double MaxLoadWeightKg { get; set; }

        public double CostPerKm { get; set; }
        public double CostPerHour { get; set; }
        public double DriverCostPerHour { get; set; }

        public double MaxDistanceWithoutRefuelKm { get; set; }
        public double MaxHeightM { get; set; }

        public string VehicleType { get; set; } = "Car";
    }
}
