using LogiMarket.Domain.Entities;
using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Builders;

// BUILDER:
// permite construir un pedido paso a paso.
public class PedidoBuilder
{
    private Cliente? _cliente;

    // Conserva los componentes y sus cantidades.
    private readonly List<(IComponenteCatalogo Componente, int Cantidad)>
        _productos = new();

    // Establece el cliente del pedido.
    public PedidoBuilder ConCliente(Cliente cliente)
    {
        if (cliente == null)
            throw new ArgumentNullException(nameof(cliente));

        _cliente = cliente;

        return this;
    }

    // Agrega un producto o kit al pedido.
    public PedidoBuilder AgregarProducto(
        IComponenteCatalogo componente,
        int cantidad)
    {
        if (componente == null)
            throw new ArgumentNullException(nameof(componente));

        if (cantidad <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(cantidad),
                "La cantidad debe ser mayor que cero.");

        _productos.Add((componente, cantidad));

        return this;
    }

   
 // Construye y devuelve el pedido terminado.
public Pedido Construir()
{
    if (_cliente == null)
        throw new InvalidOperationException(
            "Debe establecer un cliente para construir el pedido.");

    if (_productos.Count == 0)
        throw new InvalidOperationException(
            "El pedido debe contener al menos un producto.");

    Pedido pedido = new(_cliente);

    foreach (var producto in _productos)
    {
        pedido.AgregarDetalle(
            producto.Componente,
            producto.Cantidad);
    }

    // Reinicia los datos para permitir una nueva construcción.
    _productos.Clear();
    _cliente = null;

    return pedido;
}
}