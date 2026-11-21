
using LogiMarket.Domain.Enums;
using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// ESTRATEGIA CONCRETA:
// representa a los clientes que no reciben promociones.
public class PromocionNormal : IEstrategiaPromocion
{
    // Comprueba si el cliente pertenece a la categoría NORMAL.
    public bool PuedeAplicarse(
    CategoriaCliente categoria,
    int unidadesFisicas)
{
    return categoria == CategoriaCliente.NORMAL
        || (categoria == CategoriaCliente.MAYORISTA
            && unidadesFisicas < 10);
}

    // Devuelve cero porque no existe descuento.
    public decimal ObtenerPorcentajeDescuento()
    {
        return 0m;
    }
}
