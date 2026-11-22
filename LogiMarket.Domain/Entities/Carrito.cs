using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// PROTOTYPE:
// permite crear copias independientes de un carrito existente.
public class Carrito : ICarritoPrototype
{
    private readonly List<ComponentePedidoSnapshot> _productos = new();

    public IReadOnlyCollection<ComponentePedidoSnapshot> Productos =>
        _productos.AsReadOnly();

    // Agrega una copia del producto al carrito.
    public void AgregarProducto(
        IComponenteCatalogo producto,
        int cantidad)
    {
        if (producto == null)
            throw new ArgumentNullException(nameof(producto));

        if (cantidad <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(cantidad),
                "La cantidad debe ser mayor que cero.");

        ComponentePedidoSnapshot snapshot =
            ComponentePedidoSnapshot.Crear(producto, cantidad);

        _productos.Add(snapshot);
    }

    // Elimina un producto del carrito por su SKU.
    public void EliminarProducto(string sku)
    {
        ComponentePedidoSnapshot? producto =
            _productos.FirstOrDefault(
                producto => producto.SKU == sku);

        if (producto != null)
            _productos.Remove(producto);
    }

    // PROTOTYPE:
    // crea un carrito nuevo copiando los productos del original.
    public ICarritoPrototype Clonar()
    {
        Carrito carritoClonado = new();

        foreach (ComponentePedidoSnapshot producto in _productos)
        {
            ComponentePedidoSnapshot copia =
                ComponentePedidoSnapshot.CrearDesdeSnapshot(producto);

            carritoClonado._productos.Add(copia);
        }

        return carritoClonado;
    }
}