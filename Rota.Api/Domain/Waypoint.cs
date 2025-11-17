using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Rota.Api.Domain
{
    public class Waypoint
    {
        public int Id { get; set; }
        public int Order { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        #region RELACIONAMENTO

        public int RouteRequestId { get; set; }

        [JsonIgnore]
        public RouteRequest? RouteRequest { get; set; }

        #endregion
    }
}
