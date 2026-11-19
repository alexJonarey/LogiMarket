
namespace LogiMarket.Domain.Interfaces;

// COMPONENTE DEL PATRÓN COMPOSITE.
//
// Define las operaciones comunes que tendrán
// los productos individuales y los kits compuestos.
public interface IComponenteCatalogo
{
    string SKU { get; }

    string Nombre { get; }

    decimal Precio { get; }

    decimal PesoKg { get; }
}
