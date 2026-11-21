namespace LogiMarket.Domain.Interfaces;

// ITERATOR: contrato para recorrer la composición
// guardada en el snapshot de un pedido.
public interface IIteradorSnapshotComponentes
{
    bool TieneSiguiente();

    Entities.ComponentePedidoSnapshot Siguiente();
}