
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

[Fact]
public void CrearDetallePedido_ModificarKitOriginal_DebeConservarSnapshot()
{
    // Arrange: creamos los productos.
    Producto mouse = new(
        "P01", "Mouse USB", 20m, 8, 0.20m);

    Producto teclado = new(
        "P02", "Teclado", 30m, 5, 0.70m);

    Producto memoria = new(
        "P03", "Memoria USB", 10m, 20, 0.10m);

    // Creamos el kit original.
    KitCompuesto kitOficina = new(
        "KIT_OFICINA", "Kit Oficina");

    kitOficina.AgregarComponente(mouse);
    kitOficina.AgregarComponente(teclado);

    // Creamos un detalle del pedido.
    DetallePedido detalle = new(kitOficina, 1);

    // Act: modificamos el kit después de la compra.
    kitOficina.AgregarComponente(memoria);

    // Assert: el snapshot conserva la composición original.
    Assert.Equal(2, detalle.Composicion.Componentes.Count);

    Assert.Contains(
        detalle.Composicion.Componentes,
        componente => componente.SKU == "P01");

    Assert.Contains(
        detalle.Composicion.Componentes,
        componente => componente.SKU == "P02");

    Assert.DoesNotContain(
        detalle.Composicion.Componentes,
        componente => componente.SKU == "P03");
}

[Fact]
public void CrearIteradorSnapshot_ModificarKitOriginal_DebeConservarProductosOriginales()
{
    // Arrange: creamos los productos.
    Producto mouse = new(
        "P01", "Mouse USB", 20m, 8, 0.20m);

    Producto teclado = new(
        "P02", "Teclado", 30m, 5, 0.70m);

    Producto memoria = new(
        "P03", "Memoria USB", 10m, 20, 0.10m);

    // Creamos el kit original.
    KitCompuesto kitOficina = new(
        "KIT_OFICINA", "Kit Oficina");

    kitOficina.AgregarComponente(mouse);
    kitOficina.AgregarComponente(teclado);

    // Creamos el detalle del pedido.
    DetallePedido detalle = new(kitOficina, 1);

    // Modificamos el catálogo después de registrar el pedido.
    kitOficina.AgregarComponente(memoria);

    // Act: recorremos el snapshot, no el kit original.
    IIteradorSnapshotComponentes iterador =
        detalle.Composicion.CrearIteradorComponentesFisicos();

    List<string> productosEncontrados = new();

    while (iterador.TieneSiguiente())
    {
        productosEncontrados.Add(
            iterador.Siguiente().SKU);
    }

    // Assert: deben conservarse solamente los productos originales.
    Assert.Equal(
        new[] { "P01", "P02" },
        productosEncontrados);
}
[Fact]
public void CrearIteradorSnapshot_CantidadDosKits_DebeDuplicarProductos()
{
    // Arrange: crear los productos.
    Producto mouse = new("P01", "Mouse USB", 20m, 8, 0.20m);
    Producto teclado = new("P02", "Teclado", 30m, 5, 0.70m);

    // Crear un kit con ambos productos.
    KitCompuesto kitOficina = new("KIT_OFICINA", "Kit Oficina");
    kitOficina.AgregarComponente(mouse);
    kitOficina.AgregarComponente(teclado);

    // Registrar la compra de dos kits.
    DetallePedido detalle = new(kitOficina, 2);

    // Act: recorrer la composición guardada en el pedido.
    IIteradorSnapshotComponentes iterador =
        detalle.Composicion.CrearIteradorComponentesFisicos();

    List<string> productosEncontrados = new();

    while (iterador.TieneSiguiente())
    {
        productosEncontrados.Add(iterador.Siguiente().SKU);
    }

    // Assert: cada producto debe aparecer dos veces.
    Assert.Equal(
        new[] { "P01", "P01", "P02", "P02" },
        productosEncontrados);
}
[Fact]
public void DesgloseInventario_KitAnidado_DebeCalcularCantidadesFisicas()
{
    // Arrange
    Producto mouse = new("P01", "Mouse USB", 20m, 8, 0.20m);
    Producto teclado = new("P02", "Teclado", 30m, 5, 0.70m);
    Producto memoria = new("P03", "Memoria USB", 10m, 20, 0.10m);

    KitCompuesto kitOficina = new("KIT_OFICINA", "Kit Oficina");
    kitOficina.AgregarComponente(mouse);
    kitOficina.AgregarComponente(teclado);

    KitCompuesto kitTeletrabajo = new("KIT_TELETRABAJO", "Kit Teletrabajo");
    kitTeletrabajo.AgregarComponente(kitOficina);
    kitTeletrabajo.AgregarComponente(memoria);

    Cliente cliente = new("Ana", LogiMarket.Domain.Enums.CategoriaCliente.VIP);
    Pedido pedido = new(cliente);

    pedido.AgregarDetalle(kitTeletrabajo, 2);
    pedido.AgregarDetalle(memoria, 3);

    // Act
    List<ProductoInventarioDesglosado> resultado = pedido.Detalles
        .SelectMany(detalle => DesgloseInventario.Calcular(detalle))
        .GroupBy(producto => producto.SKU)
        .Select(grupo => new ProductoInventarioDesglosado(
            grupo.Key,
            grupo.First().Nombre,
            grupo.Sum(producto => producto.Cantidad)))
        .ToList();

    // Assert
    Assert.Equal(3, resultado.Count);
    Assert.Contains(resultado, producto => producto.SKU == "P01" && producto.Cantidad == 2);
    Assert.Contains(resultado, producto => producto.SKU == "P02" && producto.Cantidad == 2);
    Assert.Contains(resultado, producto => producto.SKU == "P03" && producto.Cantidad == 5);
}
[Fact]
public void DesgloseInventario_MismoSKURepetido_DebeSumarCantidades()
{
    // Arrange
    Producto memoria = new("P03", "Memoria USB", 10m, 20, 0.10m);

    KitCompuesto kit = new("KIT_MEMORIAS", "Kit Memorias");
    kit.AgregarComponente(memoria, 2);
    kit.AgregarComponente(memoria, 3);

    Cliente cliente = new("Luis", LogiMarket.Domain.Enums.CategoriaCliente.NORMAL);
    Pedido pedido = new(cliente);

    pedido.AgregarDetalle(kit, 1);

    // Act
    IReadOnlyCollection<ProductoInventarioDesglosado> resultado =
        DesgloseInventario.Calcular(pedido.Detalles.First());

    // Assert
    ProductoInventarioDesglosado producto =
        Assert.Single(resultado);

    Assert.Equal("P03", producto.SKU);
    Assert.Equal(5, producto.Cantidad);
}
}
