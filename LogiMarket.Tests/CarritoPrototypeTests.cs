using LogiMarket.Domain.Entities;
using LogiMarket.Domain.Interfaces;
using Xunit;

namespace LogiMarket.Tests;

public class CarritoPrototypeTests
{
    [Fact]
    public void Clonar_DebeConservarLosProductosOriginales()
    {
        // Arrange
        Producto mouse = new("P01", "Mouse USB", 20m, 8, 0.20m);
        Producto teclado = new("P02", "Teclado", 30m, 5, 0.70m);

        Carrito carritoOriginal = new();
        carritoOriginal.AgregarProducto(mouse, 2);
        carritoOriginal.AgregarProducto(teclado, 1);

        // Act
        Carrito carritoClonado = (Carrito)carritoOriginal.Clonar();

        // Assert
        Assert.Equal(2, carritoClonado.Productos.Count);

        Assert.Contains(
            carritoClonado.Productos,
            producto => producto.SKU == "P01");

        Assert.Contains(
            carritoClonado.Productos,
            producto => producto.SKU == "P02");
    }

    [Fact]
    public void Clonar_ModificarClon_NoDebeAfectarOriginal()
    {
        // Arrange
        Producto mouse = new("P01", "Mouse USB", 20m, 8, 0.20m);

        Carrito carritoOriginal = new();
        carritoOriginal.AgregarProducto(mouse, 2);

        Carrito carritoClonado = (Carrito)carritoOriginal.Clonar();

        // Act
        carritoClonado.AgregarProducto(
            new Producto("P03", "Memoria USB", 10m, 20, 0.10m),
            1);

        carritoClonado.EliminarProducto("P01");

        // Assert
        Assert.Single(carritoOriginal.Productos);
        Assert.Equal("P01", carritoOriginal.Productos.First().SKU);

        Assert.Single(carritoClonado.Productos);
        Assert.Equal("P03", carritoClonado.Productos.First().SKU);
    }

    [Fact]
    public void Clonar_Kit_DebeConservarSuComposicion()
    {
        // Arrange
        Producto mouse = new("P01", "Mouse USB", 20m, 8, 0.20m);
        Producto teclado = new("P02", "Teclado", 30m, 5, 0.70m);

        KitCompuesto kitOficina = new("KIT_OFICINA", "Kit Oficina");
        kitOficina.AgregarComponente(mouse, 1);
        kitOficina.AgregarComponente(teclado, 1);

        Carrito carritoOriginal = new();
        carritoOriginal.AgregarProducto(kitOficina, 1);

        // Act
        Carrito carritoClonado = (Carrito)carritoOriginal.Clonar();

        // Assert
        ComponentePedidoSnapshot kitClonado =
            carritoClonado.Productos.First();

        Assert.Equal("KIT_OFICINA", kitClonado.SKU);
        Assert.Equal(2, kitClonado.Componentes.Count);

        Assert.Contains(
            kitClonado.Componentes,
            componente => componente.SKU == "P01");

        Assert.Contains(
            kitClonado.Componentes,
            componente => componente.SKU == "P02");
    }
}