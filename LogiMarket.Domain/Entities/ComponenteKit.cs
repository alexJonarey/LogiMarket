using LogiMarket.Domain.Interfaces;
namespace LogiMarket.Domain.Entities;

// Representa un componente que forma parte de un kit.
// Permite indicar cuántas unidades contiene el kit.
public class ComponenteKit
{
    public IComponenteCatalogo Componente { get; }

    public int Cantidad { get; }

    public ComponenteKit(
        IComponenteCatalogo componente,
        int cantidad)
    {
        if (componente == null)
            throw new ArgumentNullException(nameof(componente));

        if (cantidad <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(cantidad),
                "La cantidad debe ser mayor que cero.");

        Componente = componente;
        Cantidad = cantidad;
    }
}
