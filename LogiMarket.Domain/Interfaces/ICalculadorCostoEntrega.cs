
namespace LogiMarket.Domain.Interfaces;

// PRODUCTO ABSTRACTO:
// define el contrato para calcular el costo
// de una modalidad de entrega.
public interface ICalculadorCostoEntrega
{
    decimal CalcularCosto();
}
