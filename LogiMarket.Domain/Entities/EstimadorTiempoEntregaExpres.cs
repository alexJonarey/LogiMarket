using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// PRODUCTO CONCRETO:
// estima el tiempo correspondiente a la entrega exprés.
public class EstimadorTiempoEntregaExpres
    : IEstimadorTiempoEntrega
{
    // Tiempo establecido en los requisitos del proyecto.
    private const int DiasEntrega = 1;

    public int ObtenerDiasEstimados()
    {
        return DiasEntrega;
    }
}