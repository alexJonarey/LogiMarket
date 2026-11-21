
using LogiMarket.Domain.Enums;
using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// ESTRATEGIA CONCRETA:
// implementa la promoción para clientes mayoristas.
public class PromocionMayorista : IEstrategiaPromocion
{
    // La promoción requiere al menos 10 unidades físicas.
    private const int UnidadesMinimas = 10;

    // Porcentaje de descuento mayorista: 15 %.
    private const decimal PorcentajeDescuento = 0.15m;

    // Comprueba si el cliente cumple las condiciones.
    public bool PuedeAplicarse(
        CategoriaCliente categoria,
        int unidadesFisicas)
    {
        return categoria == CategoriaCliente.MAYORISTA
            && unidadesFisicas >= UnidadesMinimas;
    }

    // Devuelve el descuento del 15 %.
    public decimal ObtenerPorcentajeDescuento()
    {
        return PorcentajeDescuento;
    }
}
