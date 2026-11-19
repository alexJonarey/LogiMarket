
using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// COMPOSITE: representa un kit que puede contener
// productos individuales u otros kits compuestos.
public class KitCompuesto : IComponenteCatalogo
{
    private readonly List<ComponenteKit> _componentes = new();

    public string SKU { get; private set; }

    public string Nombre { get; private set; }

    // El precio considera la cantidad de cada componente.
    public decimal Precio => _componentes.Sum(
        componente => componente.Componente.Precio *
                      componente.Cantidad);

    // El peso considera la cantidad de cada componente.
    public decimal PesoKg => _componentes.Sum(
        componente => componente.Componente.PesoKg *
                      componente.Cantidad);

    public IReadOnlyCollection<ComponenteKit> Componentes =>
        _componentes.AsReadOnly();

    public KitCompuesto(string sku, string nombre)
    {
        if (string.IsNullOrWhiteSpace(sku))
            throw new ArgumentException("El SKU del kit es obligatorio.");

        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del kit es obligatorio.");

        SKU = sku.Trim();
        Nombre = nombre.Trim();
    }

    // Agrega un producto o kit indicando su cantidad.
    public void AgregarComponente(
        IComponenteCatalogo componente,
        int cantidad = 1)
    {
        if (componente == null)
            throw new ArgumentNullException(nameof(componente));

        if (ReferenceEquals(componente, this))
            throw new InvalidOperationException(
                "Un kit no puede contenerse a sí mismo.");

        // Evita ciclos indirectos entre kits.
        if (componente is KitCompuesto kit &&
            kit.ContieneKit(this))
        {
            throw new InvalidOperationException(
                "No se puede agregar el kit porque generaría un ciclo.");
        }

        ComponenteKit nuevoComponente = new(componente, cantidad);

        _componentes.Add(nuevoComponente);
    }

    // Comprueba recursivamente si este kit contiene
    // directa o indirectamente al kit buscado.
    private bool ContieneKit(KitCompuesto kitBuscado)
    {
        if (ReferenceEquals(this, kitBuscado))
            return true;

        foreach (ComponenteKit componente in _componentes)
        {
            if (componente.Componente is KitCompuesto kit &&
                kit.ContieneKit(kitBuscado))
            {
                return true;
            }
        }

        return false;
    }

    // Permite recorrer los productos físicos del kit
// mediante el patrón Iterator.
public IIteradorComponentes CrearIteradorComponentes()
{
    return new IteradorComponentesFisicos(this);
}
}
