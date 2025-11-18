using Rota.Api.Domain;

namespace Rota.Api.Services
{
    public interface IRouteCalculator
    {
        /// <summary>
        /// Calcula a rota (não persiste). Lança RouteCalculationException se inválida.
        /// </summary>
        RouteResult Calculate(RouteRequest request);
    }
}
