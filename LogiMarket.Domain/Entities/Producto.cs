using LogiMarket.Domain.Interfaces;
namespace LogiMarket.Domain.Entities;

// ENTIDAD: representa un producto individual del catálogo.
// Sus reglas de validación se mantienen dentro del dominio.
public class Producto : IComponenteCatalogo
{
    public int Id { get; private set; }

    public string SKU { get; private set; }

    public string Nombre { get; private set; }

    public decimal Precio { get; private set; }

    public int Stock { get; private set; }

    public decimal PesoKg { get; private set; }

    // Constructor privado para permitir que EF Core
    // reconstruya la entidad desde la base de datos.
    private Producto()
    {
        SKU = string.Empty;
        Nombre = string.Empty;
    }

    // Constructor público para crear productos válidos.
    public Producto(
        string sku,
        string nombre,
        decimal precio,
        int stock,
        decimal pesoKg)
    {
        if (string.IsNullOrWhiteSpace(sku))
            throw new ArgumentException("El SKU es obligatorio.");

        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre es obligatorio.");

        if (precio <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(precio),
                "El precio debe ser mayor que cero.");

        if (stock < 0)
            throw new ArgumentOutOfRangeException(
                nameof(stock),
                "El stock no puede ser negativo.");

        if (pesoKg < 0)
            throw new ArgumentOutOfRangeException(
                nameof(pesoKg),
                "El peso no puede ser negativo.");

        SKU = sku.Trim();
        Nombre = nombre.Trim();
        Precio = precio;
        Stock = stock;
        PesoKg = pesoKg;
    }
    // Permite actualizar el precio del producto en el catálogo.
public void CambiarPrecio(decimal nuevoPrecio)
{
    if (nuevoPrecio <= 0)
        throw new ArgumentOutOfRangeException(
            nameof(nuevoPrecio),
            "El precio debe ser mayor que cero.");

    Precio = nuevoPrecio;
}
}
