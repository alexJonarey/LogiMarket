using LogiMarket.Domain.Interfaces;
namespace LogiMarket.Domain.Entities;

// Resultado del desglose: representa un producto físico
// y la cantidad total necesaria para atender el pedido.
public sealed record ProductoInventarioDesglosado(
    string SKU,
    string Nombre,
    int Cantidad);

// DESGLOSE DE INVENTARIO:
// utiliza Iterator para recorrer el snapshot del pedido
// y agrupa los productos físicos por SKU.
public static class DesgloseInventario
{
    public static IReadOnlyCollection<ProductoInventarioDesglosado> Calcular(
        DetallePedido detalle)
    {
        if (detalle == null)
            throw new ArgumentNullException(nameof(detalle));

        // Se utiliza el iterador del snapshot, no el catálogo actual.
        IIteradorSnapshotComponentes iterador =
            detalle.Composicion.CrearIteradorComponentesFisicos();

        List<ProductoInventarioDesglosado> resultado = new();

        // Guarda la posición de cada SKU para acumular sus cantidades.
        Dictionary<string, int> posicionesPorSKU = new();

        while (iterador.TieneSiguiente())
        {
            ComponentePedidoSnapshot componente = iterador.Siguiente();

            if (posicionesPorSKU.TryGetValue(
                componente.SKU,
                out int posicion))
            {
                // Si el SKU ya existe, incrementamos su cantidad.
                ProductoInventarioDesglosado productoExistente =
                    resultado[posicion];

                resultado[posicion] = productoExistente with
                {
                    Cantidad = productoExistente.Cantidad + 1
                };
            }
            else
            {
                // Si es la primera aparición del SKU, lo registramos.
                posicionesPorSKU.Add(
                    componente.SKU,
                    resultado.Count);

                resultado.Add(
                    new ProductoInventarioDesglosado(
                        componente.SKU,
                        componente.Nombre,
                        1));
            }
        }

        return resultado.AsReadOnly();
    }
}
