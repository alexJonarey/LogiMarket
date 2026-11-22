using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// PRODUCTO CONCRETO:
// calcula el costo correspondiente a la entrega exprés.
public class CalculadorCostoEntregaExpres
    : ICalculadorCostoEntrega
{
    // Costo establecido en los requisitos del proyecto.
    private const decimal CostoEntrega = 12m;

    public decimal CalcularCosto()
    {
        return CostoEntrega;
    }
}