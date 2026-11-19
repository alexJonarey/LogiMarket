
using LogiMarket.Domain.Enums;

namespace LogiMarket.Domain.Entities;

// ENTIDAD: representa a un cliente del sistema.
// La categoría permitirá aplicar posteriormente
// las estrategias de promoción correspondientes.
public class Cliente
{
    public int Id { get; private set; }

    public string Nombre { get; private set; }

    public CategoriaCliente Categoria { get; private set; }

    // Constructor privado para permitir que EF Core
    // reconstruya la entidad desde la base de datos.
    private Cliente()
    {
        Nombre = string.Empty;
    }

    // Constructor público para crear clientes válidos.
    public Cliente(string nombre, CategoriaCliente categoria)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del cliente es obligatorio.");

        Nombre = nombre.Trim();
        Categoria = categoria;
    }
}
