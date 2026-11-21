
namespace LogiMarket.Domain.Entities;

// Resultado del cálculo de una promoción.
public sealed record ResultadoPromocion(
    int UnidadesFisicas,
    decimal PorcentajeDescuento,
    decimal Descuento);

// CALCULADORA DE PROMOCIONES:
// conecta los pedidos con el patrón Strategy.
public class CalculadoraPromocion
{
    private readonly SelectorPromocion _selectorPromocion = new();

    public ResultadoPromocion Calcular(Pedido pedido)
    {
        if (pedido == null)
            throw new ArgumentNullException(nameof(pedido));

        // Cuenta las unidades físicas de todos los detalles.
        int unidadesFisicas = 0;

        foreach (DetallePedido detalle in pedido.Detalles)
        {
            IReadOnlyCollection<ProductoInventarioDesglosado> productos =
                DesgloseInventario.Calcular(detalle);

            unidadesFisicas += productos.Sum(
                producto => producto.Cantidad);
        }

        // Selecciona la estrategia según el cliente y las unidades.
        var estrategia = _selectorPromocion.Seleccionar(
            pedido.Cliente.Categoria,
            unidadesFisicas);

        decimal porcentaje = estrategia.ObtenerPorcentajeDescuento();

        // Calcula el descuento y redondea a dos decimales.
        decimal descuento = decimal.Round(
            pedido.Subtotal * porcentaje,
            2,
            MidpointRounding.AwayFromZero);

        return new ResultadoPromocion(
            unidadesFisicas,
            porcentaje,
            descuento);
    }
}
