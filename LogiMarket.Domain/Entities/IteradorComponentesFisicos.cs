
using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// ITERATOR CONCRETO:
// recorre los productos físicos de un kit,
// incluyendo los componentes de kits anidados.
public class IteradorComponentesFisicos : IIteradorComponentes
{
    private readonly List<IComponenteCatalogo> _componentesFisicos = new();

    private int _posicionActual;

    public IteradorComponentesFisicos(
        IComponenteCatalogo componenteRaiz)
    {
        if (componenteRaiz == null)
            throw new ArgumentNullException(nameof(componenteRaiz));

        ExpandirComponente(componenteRaiz, 1);
    }

    // Expande recursivamente los kits hasta encontrar productos físicos.
    private void ExpandirComponente(
        IComponenteCatalogo componente,
        int cantidadAcumulada)
    {
        if (componente is Producto)
        {
            for (int i = 0; i < cantidadAcumulada; i++)
            {
                _componentesFisicos.Add(componente);
            }

            return;
        }

        if (componente is KitCompuesto kit)
        {
            foreach (ComponenteKit componenteKit in kit.Componentes)
            {
                int cantidadTotal =
                    cantidadAcumulada * componenteKit.Cantidad;

                ExpandirComponente(
                    componenteKit.Componente,
                    cantidadTotal);
            }

            return;
        }

        throw new InvalidOperationException(
            "El componente no es un producto ni un kit válido.");
    }

    // Comprueba si quedan productos por recorrer.
    public bool TieneSiguiente()
    {
        return _posicionActual < _componentesFisicos.Count;
    }

    // Devuelve el siguiente producto físico.
    public IComponenteCatalogo Siguiente()
    {
        if (!TieneSiguiente())
        {
            throw new InvalidOperationException(
                "No existen más componentes físicos por recorrer.");
        }

        return _componentesFisicos[_posicionActual++];
    }
}
