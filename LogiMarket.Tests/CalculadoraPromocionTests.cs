
using LogiMarket.Domain.Entities;
using LogiMarket.Domain.Enums;

namespace LogiMarket.Tests;

// Comprueba el cálculo monetario de las promociones.
public class CalculadoraPromocionTests
{
    [Fact]
    public void Calcular_MayoristaConDiezMemorias_DebeDescontar15()
    {
        // Arrange
        Producto memoria = new("P03", "Memoria USB", 10m, 20, 0.10m);

        Cliente cliente = new("Carmen", CategoriaCliente.MAYORISTA);
        Pedido pedido = new(cliente);

        pedido.AgregarDetalle(memoria, 10);

        CalculadoraPromocion calculadora = new();

        // Act
        ResultadoPromocion resultado = calculadora.Calcular(pedido);

        // Assert
        Assert.Equal(10, resultado.UnidadesFisicas);
        Assert.Equal(0.15m, resultado.PorcentajeDescuento);
        Assert.Equal(15m, resultado.Descuento);
    }

    [Fact]
    public void Calcular_VIPConUnKit_DebeDescontar10PorCiento()
    {
        // Arrange
        Producto mouse = new("P01", "Mouse USB", 20m, 8, 0.20m);
        Producto teclado = new("P02", "Teclado", 30m, 5, 0.70m);

        KitCompuesto kitOficina = new("KIT_OFICINA", "Kit Oficina");
        kitOficina.AgregarComponente(mouse);
        kitOficina.AgregarComponente(teclado);

        Cliente cliente = new("Ana", CategoriaCliente.VIP);
        Pedido pedido = new(cliente);

        pedido.AgregarDetalle(kitOficina, 1);

        CalculadoraPromocion calculadora = new();

        // Act
        ResultadoPromocion resultado = calculadora.Calcular(pedido);

        // Assert
        Assert.Equal(2, resultado.UnidadesFisicas);
        Assert.Equal(0.10m, resultado.PorcentajeDescuento);
        Assert.Equal(5m, resultado.Descuento);
    }

    [Fact]
    public void Calcular_MayoristaConNueveUnidades_DebeAplicarCero()
    {
        // Arrange
        Producto memoria = new("P03", "Memoria USB", 10m, 20, 0.10m);

        Cliente cliente = new("Carmen", CategoriaCliente.MAYORISTA);
        Pedido pedido = new(cliente);

        pedido.AgregarDetalle(memoria, 9);

        CalculadoraPromocion calculadora = new();

        // Act
        ResultadoPromocion resultado = calculadora.Calcular(pedido);

        // Assert
        Assert.Equal(9, resultado.UnidadesFisicas);
        Assert.Equal(0m, resultado.PorcentajeDescuento);
        Assert.Equal(0m, resultado.Descuento);
    }

    [Fact]
    public void Calcular_PedidoNulo_DebeLanzarExcepcion()
    {
        CalculadoraPromocion calculadora = new();

        Assert.Throws<ArgumentNullException>(() =>
            calculadora.Calcular(null!));
    }
}
