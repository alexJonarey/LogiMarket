
using LogiMarket.Domain.Enums;
using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// SELECTOR DE ESTRATEGIAS:
// selecciona la promoción que corresponde al cliente.
// Respeta la prioridad de MAYORISTA sobre VIP.
public class SelectorPromocion
{
    private readonly List<IEstrategiaPromocion> _estrategias;

    public SelectorPromocion()
    {
        // El orden establece la prioridad de las promociones.
        _estrategias = new List<IEstrategiaPromocion>
        {
            new PromocionMayorista(),
            new PromocionVIP(),
            new PromocionNormal()
        };
    }

    // Busca la primera estrategia que cumpla las condiciones.
    public IEstrategiaPromocion Seleccionar(
        CategoriaCliente categoria,
        int unidadesFisicas)
    {
        if (unidadesFisicas < 0)
            throw new ArgumentOutOfRangeException(nameof(unidadesFisicas));

        foreach (IEstrategiaPromocion estrategia in _estrategias)
        {
            if (estrategia.PuedeAplicarse(categoria, unidadesFisicas))
            {
                return estrategia;
            }
        }

        throw new InvalidOperationException(
            "No existe una estrategia para la categoría indicada.");
    }
}
