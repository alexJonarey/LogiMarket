using LogiMarket.Domain.Enums;
using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;



// ENTIDAD: representa un pedido realizado por un cliente.
public class Pedido
{
    private readonly List<DetallePedido> _detalles = new();

    public int Id { get; private set; }

    public int ClienteId { get; private set; }

    public Cliente Cliente { get; private set; } = null!;

    public EstadoPedido Estado { get; private set; }

    // Expone los detalles sin permitir modificar
    // directamente la colección desde fuera.
    public IReadOnlyCollection<DetallePedido> Detalles =>
        _detalles.AsReadOnly();

    // Calcula el subtotal sumando los detalles.
    public decimal Subtotal => _detalles.Sum(
        detalle => detalle.Subtotal);

    // Constructor privado para EF Core.
    private Pedido()
    {
    }

    // Constructor público para crear un pedido.
    public Pedido(Cliente cliente)
    {
        if (cliente == null)
            throw new ArgumentNullException(nameof(cliente));

        Cliente = cliente;
        ClienteId = cliente.Id;
        Estado = EstadoPedido.BORRADOR;
    }

   
    // Agrega un producto individual o un kit al pedido.
    public void AgregarDetalle(
        IComponenteCatalogo componente,
        int cantidad)
    {
        if (Estado != EstadoPedido.BORRADOR)
        {
            throw new InvalidOperationException(
                "Solo se pueden agregar productos a pedidos en BORRADOR.");
        }

        if (componente == null)
            throw new ArgumentNullException(nameof(componente));

        DetallePedido detalle = new(componente, cantidad);

        _detalles.Add(detalle);
    }


    // Controla las transiciones permitidas del pedido.
    public void CambiarEstado(EstadoPedido nuevoEstado)
    {
        bool transicionPermitida =
            (Estado == EstadoPedido.BORRADOR &&
             (nuevoEstado == EstadoPedido.VALIDADO ||
              nuevoEstado == EstadoPedido.CANCELADO))

            ||

            (Estado == EstadoPedido.VALIDADO &&
             (nuevoEstado == EstadoPedido.PAGADO ||
              nuevoEstado == EstadoPedido.CANCELADO))

            ||

            (Estado == EstadoPedido.PAGADO &&
             nuevoEstado == EstadoPedido.DESPACHADO)

            ||

            (Estado == EstadoPedido.DESPACHADO &&
             nuevoEstado == EstadoPedido.ENTREGADO);

        if (!transicionPermitida)
        {
            throw new InvalidOperationException(
                $"No se permite cambiar de {Estado} a {nuevoEstado}.");
        }

        Estado = nuevoEstado;
    }
}

