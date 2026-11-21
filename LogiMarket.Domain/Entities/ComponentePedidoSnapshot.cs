
using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// SNAPSHOT:
// conserva una copia de un producto o kit
// y de su composición en el momento de la compra.
public class ComponentePedidoSnapshot
{
    private readonly List<ComponentePedidoSnapshot> _componentes = new();

    public string SKU { get; }

    public string Nombre { get; }

    public int Cantidad { get; }

    public decimal PrecioUnitario { get; }

    public decimal PesoUnitarioKg { get; }

    public IReadOnlyCollection<ComponentePedidoSnapshot> Componentes =>
        _componentes.AsReadOnly();

    private ComponentePedidoSnapshot(
        string sku,
        string nombre,
        int cantidad,
        decimal precioUnitario,
        decimal pesoUnitarioKg)
    {
        SKU = sku;
        Nombre = nombre;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
        PesoUnitarioKg = pesoUnitarioKg;
    }

    // Crea una copia independiente del componente y su composición.
    public static ComponentePedidoSnapshot Crear(
        IComponenteCatalogo componente,
        int cantidad = 1)
    {
        if (componente == null)
            throw new ArgumentNullException(nameof(componente));

        if (cantidad <= 0)
            throw new ArgumentOutOfRangeException(nameof(cantidad));

        ComponentePedidoSnapshot snapshot = new(
            componente.SKU,
            componente.Nombre,
            cantidad,
            componente.Precio,
            componente.PesoKg);

        // Si es un kit, copia también todos sus componentes.
        if (componente is KitCompuesto kit)
        {
            foreach (ComponenteKit componenteKit in kit.Componentes)
            {
                ComponentePedidoSnapshot componenteSnapshot =
                    Crear(
                        componenteKit.Componente,
                        componenteKit.Cantidad);

                snapshot._componentes.Add(componenteSnapshot);
            }
        }

        return snapshot;
    }
}
