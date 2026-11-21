
using LogiMarket.Domain.Enums;

// STRATEGY:
// define el contrato común para las estrategias
// que calculan las promociones de los clientes.
namespace LogiMarket.Domain.Interfaces
{
    public interface IEstrategiaPromocion
    {
        // Indica si la estrategia puede aplicarse
        // según la categoría y las unidades físicas.
        bool PuedeAplicarse(
            CategoriaCliente categoria,
            int unidadesFisicas);

        // Devuelve el porcentaje de descuento.
        decimal ObtenerPorcentajeDescuento();
    }
}
