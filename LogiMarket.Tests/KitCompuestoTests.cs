
using LogiMarket.Domain.Entities;
using LogiMarket.Domain.Interfaces;
namespace LogiMarket.Tests;

// Comprueba el funcionamiento de Composite e Iterator.
public class KitCompuestoTests
{
    [Fact]
    public void CrearIterador_KitOficina_DebeDevolverMouseYTeclado()
    {
        // Arrange: creamos los productos.
        Producto mouse = new(
            "P01", "Mouse USB", 20m, 8, 0.20m);

        Producto teclado = new(
            "P02", "Teclado", 30m, 5, 0.70m);

        // Creamos el kit con sus componentes.
        KitCompuesto kitOficina = new(
            "KIT_OFICINA", "Kit Oficina");

        kitOficina.AgregarComponente(mouse);
        kitOficina.AgregarComponente(teclado);

        // Act: recorremos los productos físicos.
        IIteradorComponentes iterador =
            kitOficina.CrearIteradorComponentes();

        List<string> productosEncontrados = new();

        while (iterador.TieneSiguiente())
        {
            productosEncontrados.Add(
                iterador.Siguiente().SKU);
        }

        // Assert: comprobamos el resultado.
        Assert.Equal(
            new[] { "P01", "P02" },
            productosEncontrados);
    }

    [Fact]
    public void CrearIterador_KitAnidado_DebeExpandirSusComponentes()
    {
        // Arrange: creamos los productos.
        Producto mouse = new(
            "P01", "Mouse USB", 20m, 8, 0.20m);

        Producto teclado = new(
            "P02", "Teclado", 30m, 5, 0.70m);

        Producto memoria = new(
            "P03", "Memoria USB", 10m, 20, 0.10m);

        // Creamos KIT_OFICINA.
        KitCompuesto kitOficina = new(
            "KIT_OFICINA", "Kit Oficina");

        kitOficina.AgregarComponente(mouse);
        kitOficina.AgregarComponente(teclado);

        // Creamos KIT_TELETRABAJO.
        KitCompuesto kitTeletrabajo = new(
            "KIT_TELETRABAJO", "Kit Teletrabajo");

        kitTeletrabajo.AgregarComponente(kitOficina);
        kitTeletrabajo.AgregarComponente(memoria);

        // Act: recorremos el kit anidado.
        IIteradorComponentes iterador =
            kitTeletrabajo.CrearIteradorComponentes();

        List<string> productosEncontrados = new();

        while (iterador.TieneSiguiente())
        {
            productosEncontrados.Add(
                iterador.Siguiente().SKU);
        }

        // Assert: debe expandir los kits anidados.
        Assert.Equal(
            new[] { "P01", "P02", "P03" },
            productosEncontrados);
    }

    [Fact]
    public void CrearIterador_CantidadMayorAUno_DebeRepetirProductos()
    {
        // Arrange: creamos un producto.
        Producto memoria = new(
            "P03", "Memoria USB", 10m, 20, 0.10m);

        KitCompuesto kit = new(
            "KIT_MEMORIAS", "Kit Memorias");

        kit.AgregarComponente(memoria, 3);

        // Act: recorremos sus componentes.
        IIteradorComponentes iterador =
            kit.CrearIteradorComponentes();

        List<string> productosEncontrados = new();

        while (iterador.TieneSiguiente())
        {
            productosEncontrados.Add(
                iterador.Siguiente().SKU);
        }

        // Assert: deben aparecer tres memorias.
        Assert.Equal(
            new[] { "P03", "P03", "P03" },
            productosEncontrados);
    }
}
