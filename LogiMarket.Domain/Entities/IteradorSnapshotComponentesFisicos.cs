
using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// ITERATOR CONCRETO:
// recorre los productos físicos del snapshot de un pedido,
// sin depender del catálogo actual.
public class IteradorSnapshotComponentesFisicos
    : IIteradorSnapshotComponentes
{
    private readonly List<ComponentePedidoSnapshot> _componentesFisicos = new();

    private int _posicionActual;

    public IteradorSnapshotComponentesFisicos(
        ComponentePedidoSnapshot componenteRaiz)
    {
        if (componenteRaiz == null)
            throw new ArgumentNullException(nameof(componenteRaiz));

        ExpandirComponente(componenteRaiz, 1);
    }

    // Expande kits anidados usando exclusivamente
    // la composición guardada en el snapshot.
    private void ExpandirComponente(
        ComponentePedidoSnapshot componente,
        int cantidadAcumulada)
    {
        if (componente.Componentes.Count == 0)
        {
            for (int i = 0; i < cantidadAcumulada; i++)
            {
                _componentesFisicos.Add(componente);
            }

            return;
        }

        foreach (ComponentePedidoSnapshot componenteHijo
                 in componente.Componentes)
        {
            int cantidadTotal =
                cantidadAcumulada * componenteHijo.Cantidad;

            ExpandirComponente(
                componenteHijo,
                cantidadTotal);
        }
    }

    // Comprueba si quedan componentes físicos por recorrer.
    public bool TieneSiguiente()
    {
        return _posicionActual < _componentesFisicos.Count;
    }

    // Devuelve el siguiente componente físico del snapshot.
    public ComponentePedidoSnapshot Siguiente()
    {
        if (!TieneSiguiente())
        {
            throw new InvalidOperationException(
                "No existen más componentes físicos por recorrer.");
        }

        return _componentesFisicos[_posicionActual++];
    }
}
