
using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// COMPOSITE: representa un kit que puede contener
// productos individuales u otros kits compuestos.
public class KitCompuesto : IComponenteCatalogo
{
    private readonly List<IComponenteCatalogo> _componentes = new();

    public string SKU { get; private set; }

    public string Nombre { get; private set; }

    // El precio del kit se obtiene sumando
    // los precios de sus componentes.
    public decimal Precio => _componentes.Sum(
        componente => componente.Precio);

    // El peso se obtiene sumando los pesos
    // de sus componentes.
    public decimal PesoKg => _componentes.Sum(
        componente => componente.PesoKg);

    public IReadOnlyCollection<IComponenteCatalogo> Componentes =>
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

    // Agrega un producto o kit al kit actual.
   
    // Agrega un producto o kit al kit actual.
    public void AgregarComponente(IComponenteCatalogo componente)
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

        _componentes.Add(componente);
    }

    // Comprueba recursivamente si este kit contiene
    // directa o indirectamente al kit buscado.
    private bool ContieneKit(KitCompuesto kitBuscado)
    {
        if (ReferenceEquals(this, kitBuscado))
            return true;

        foreach (IComponenteCatalogo componente in _componentes)
        {
            if (componente is KitCompuesto kit &&
                kit.ContieneKit(kitBuscado))
            {
                return true;
            }
        }

        return false;
    }

}
