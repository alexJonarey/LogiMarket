using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// PRODUCTO CONCRETO:
// estima el tiempo correspondiente a la entrega estándar.
public class EstimadorTiempoEntregaEstandar
    : IEstimadorTiempoEntrega
{
    // Tiempo establecido en los requisitos del proyecto.
    private const int DiasEntrega = 3;

    public int ObtenerDiasEstimados()
    {
        return DiasEntrega;
    }
}