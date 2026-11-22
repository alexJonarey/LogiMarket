using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// FÁBRICA CONCRETA:
// crea los servicios relacionados con la entrega estándar.
public class FabricaEntregaEstandar : IFabricaEntrega
{
    // Crea el calculador que devuelve un costo de $5.
    public ICalculadorCostoEntrega CrearCalculadorCosto()
    {
        return new CalculadorCostoEntregaEstandar();
    }

    // Crea el estimador que devuelve 3 días.
    public IEstimadorTiempoEntrega CrearEstimadorTiempo()
    {
        return new EstimadorTiempoEntregaEstandar();
    }
}