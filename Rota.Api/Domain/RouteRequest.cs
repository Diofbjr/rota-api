using System;
using System.Collections.Generic;

namespace Rota.Api.Domain
{
    public class RouteRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Tipo de otimização (distância, tempo, custo…)
        public string Optimization { get; set; } = "distance";

        // Waypoints da rota
        public List<Waypoint> Waypoints { get; set; } = new();

        #region CARGA
        public double TotalLoadWeightKg { get; set; }
        public double TotalLoadVolumeM3 { get; set; }
        #endregion

        #region VEÍCULO
        public int? VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
        #endregion

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<RouteResult>? Results { get; set; }
    }
}
