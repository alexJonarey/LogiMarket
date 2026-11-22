using LogiMarket.Domain.Builders;
using LogiMarket.Domain.Entities;
using LogiMarket.Domain.Enums;
using Xunit;

namespace LogiMarket.Tests;

public class PedidoBuilderTests
{
    [Fact]
    public void Construir_ConClienteYProductos_DebeCrearPedido()
    {
        // Arrange
        Cliente cliente = new(
            "Ana",
            CategoriaCliente.VIP);

        Producto mouse = new(
            "P01", "Mouse USB", 20m, 8, 0.20m);

        Producto teclado = new(
            "P02", "Teclado", 30m, 5, 0.70m);

        // Act
        Pedido pedido = new PedidoBuilder()
            .ConCliente(cliente)
            .AgregarProducto(mouse, 2)
            .AgregarProducto(teclado, 1)
            .Construir();

        // Assert
        Assert.Equal(cliente, pedido.Cliente);
        Assert.Equal(2, pedido.Detalles.Count);
        Assert.Equal(70m, pedido.Subtotal);
        Assert.Equal(EstadoPedido.BORRADOR, pedido.Estado);
    }

    [Fact]
    public void Construir_SinCliente_DebeLanzarExcepcion()
    {
        // Arrange
        PedidoBuilder builder = new();

        Producto mouse = new(
            "P01", "Mouse USB", 20m, 8, 0.20m);

        builder.AgregarProducto(mouse, 1);

        // Act y Assert
        Assert.Throws<InvalidOperationException>(
            () => builder.Construir());
    }

    [Fact]
    public void Construir_SinProductos_DebeLanzarExcepcion()
    {
        // Arrange
        Cliente cliente = new(
            "Ana",
            CategoriaCliente.VIP);

        PedidoBuilder builder = new();

        builder.ConCliente(cliente);

        // Act y Assert
        Assert.Throws<InvalidOperationException>(
            () => builder.Construir());
    }

    [Fact]
    public void AgregarProducto_CantidadCero_DebeLanzarExcepcion()
    {
        // Arrange
        Producto mouse = new(
            "P01", "Mouse USB", 20m, 8, 0.20m);

        PedidoBuilder builder = new();

        // Act y Assert
        Assert.Throws<ArgumentOutOfRangeException>(
            () => builder.AgregarProducto(mouse, 0));
    }
    [Fact]
public void Construir_DosPedidos_DebeConservarLosProductosDeCadaConstruccion()
{
    // Arrange
    Cliente cliente = new(
        "Ana",
        CategoriaCliente.VIP);

    Producto mouse = new(
        "P01", "Mouse USB", 20m, 8, 0.20m);

    Producto teclado = new(
        "P02", "Teclado", 30m, 5, 0.70m);

    PedidoBuilder builder = new();

    // Act
    Pedido primerPedido = builder
        .ConCliente(cliente)
        .AgregarProducto(mouse, 1)
        .Construir();

    Pedido segundoPedido = builder
        .ConCliente(cliente)
        .AgregarProducto(teclado, 1)
        .Construir();

    // Assert
    Assert.Single(primerPedido.Detalles);
    Assert.Single(segundoPedido.Detalles);

    Assert.Equal(20m, primerPedido.Subtotal);
    Assert.Equal(30m, segundoPedido.Subtotal);
}
[Fact]
public void Construir_ModificarKitOriginal_DebeConservarSnapshotDelPedido()
{
    // Arrange: creamos los productos.
    Producto mouse = new(
        "P01", "Mouse USB", 20m, 8, 0.20m);

    Producto teclado = new(
        "P02", "Teclado", 30m, 5, 0.70m);

    Producto memoria = new(
        "P03", "Memoria USB", 10m, 20, 0.10m);

    // Creamos un kit con dos productos.
    KitCompuesto kit = new(
        "KIT_OFICINA", "Kit Oficina");

    kit.AgregarComponente(mouse);
    kit.AgregarComponente(teclado);

    Cliente cliente = new(
        "Ana", CategoriaCliente.VIP);

    // Act: construimos el pedido.
    Pedido pedido = new PedidoBuilder()
        .ConCliente(cliente)
        .AgregarProducto(kit, 1)
        .Construir();

    // Modificamos posteriormente el kit original.
    kit.AgregarComponente(memoria);

    // Assert: el pedido conserva la composición original.
    DetallePedido detalle = Assert.Single(pedido.Detalles);

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
public void Construir_ModificarPrecioDelProducto_DebeConservarPrecioOriginal()
{
    // Arrange
    Producto mouse = new(
        "P01", "Mouse USB", 20m, 8, 0.20m);

    Cliente cliente = new(
        "Ana", CategoriaCliente.VIP);

    // Act
    Pedido pedido = new PedidoBuilder()
        .ConCliente(cliente)
        .AgregarProducto(mouse, 2)
        .Construir();

    // Assert: modificamos el precio del producto original.
    // Para esta prueba necesitamos que exista un método
    // de modificación de precio en Producto.
}

}