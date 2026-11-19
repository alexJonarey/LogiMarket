
using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// ENTIDAD: representa una línea de un pedido.
// Conserva una instantánea del componente comprado.
public class DetallePedido
{
    public int Id { get; private set; }

    public int PedidoId { get; private set; }

    public string SKU { get; private set; }

    public string NombreProducto { get; private set; }

    public int Cantidad { get; private set; }

    public decimal PrecioUnitario { get; private set; }

    public decimal PesoUnitarioKg { get; private set; }

    public decimal Subtotal => PrecioUnitario * Cantidad;

    // Constructor privado para EF Core.
    private DetallePedido()
    {
        SKU = string.Empty;
        NombreProducto = string.Empty;
    }

    // Permite registrar productos individuales o kits.
    public DetallePedido(
        IComponenteCatalogo componente,
        int cantidad)
    {
        if (componente == null)
            throw new ArgumentNullException(nameof(componente));

        if (cantidad <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(cantidad),
                "La cantidad debe ser mayor que cero.");

        SKU = componente.SKU;
        NombreProducto = componente.Nombre;
        Cantidad = cantidad;
        PrecioUnitario = componente.Precio;
        PesoUnitarioKg = componente.PesoKg;
    }
}
