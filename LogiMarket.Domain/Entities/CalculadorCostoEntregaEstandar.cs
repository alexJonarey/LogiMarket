using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// PRODUCTO CONCRETO:
// calcula el costo correspondiente a la entrega estándar.
public class CalculadorCostoEntregaEstandar
    : ICalculadorCostoEntrega
{
    // Costo establecido en los requisitos del proyecto.
    private const decimal CostoEntrega = 5m;

    public decimal CalcularCosto()
    {
        return CostoEntrega;
    }
}