
using LogiMarket.Domain.Enums;
using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// ESTRATEGIA CONCRETA:
// implementa la promoción para clientes VIP.
public class PromocionVIP : IEstrategiaPromocion
{
    // Porcentaje de descuento VIP: 10 %.
    private const decimal PorcentajeDescuento = 0.10m;

    // Comprueba si el cliente pertenece a la categoría VIP.
    public bool PuedeAplicarse(
        CategoriaCliente categoria,
        int unidadesFisicas)
    {
        return categoria == CategoriaCliente.VIP;
    }

    // Devuelve el descuento del 10 %.
    public decimal ObtenerPorcentajeDescuento()
    {
        return PorcentajeDescuento;
    }
}
