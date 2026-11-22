using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// FÁBRICA CONCRETA:
// crea los servicios relacionados con la entrega exprés.
public class FabricaEntregaExpres : IFabricaEntrega
{
    // Crea el calculador que devuelve un costo de $12.
    public ICalculadorCostoEntrega CrearCalculadorCosto()
    {
        return new CalculadorCostoEntregaExpres();
    }

    // Crea el estimador que devuelve 1 día.
    public IEstimadorTiempoEntrega CrearEstimadorTiempo()
    {
        return new EstimadorTiempoEntregaExpres();
    }
}