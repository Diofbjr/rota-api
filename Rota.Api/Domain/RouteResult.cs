using System;

namespace Rota.Api.Domain
{
    public class RouteResult
    {
        public int Id { get; set; }

        public double TotalDistanceKm { get; set; }
        public double TotalTimeMinutes { get; set; }
        public double? TotalCost { get; set; }

        public string SerializedPath { get; set; } = string.Empty;

        public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;

        #region RELACIONAMENTO
        public int RouteRequestId { get; set; }
        public RouteRequest? RouteRequest { get; set; }
        #endregion
    }
}
