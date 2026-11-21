
using LogiMarket.Domain.Entities;
using LogiMarket.Domain.Enums;

namespace LogiMarket.Tests;

// Comprueba la selección de estrategias de promoción.
public class SelectorPromocionTests
{
    [Fact]
    public void Seleccionar_MayoristaConDiezUnidades_DebeAplicar15PorCiento()
    {
        SelectorPromocion selector = new();

        var estrategia = selector.Seleccionar(
            CategoriaCliente.MAYORISTA,
            10);

        Assert.IsType<PromocionMayorista>(estrategia);
        Assert.Equal(0.15m, estrategia.ObtenerPorcentajeDescuento());
    }

    [Fact]
    public void Seleccionar_MayoristaConMenosDeDiezUnidades_DebeAplicarCero()
    {
        SelectorPromocion selector = new();

        var estrategia = selector.Seleccionar(
            CategoriaCliente.MAYORISTA,
            5);

        Assert.IsType<PromocionNormal>(estrategia);
        Assert.Equal(0m, estrategia.ObtenerPorcentajeDescuento());
    }

    [Fact]
    public void Seleccionar_ClienteVIP_DebeAplicar10PorCiento()
    {
        SelectorPromocion selector = new();

        var estrategia = selector.Seleccionar(
            CategoriaCliente.VIP,
            2);

        Assert.IsType<PromocionVIP>(estrategia);
        Assert.Equal(0.10m, estrategia.ObtenerPorcentajeDescuento());
    }

    [Fact]
    public void Seleccionar_ClienteNormal_DebeAplicarCero()
    {
        SelectorPromocion selector = new();

        var estrategia = selector.Seleccionar(
            CategoriaCliente.NORMAL,
            20);

        Assert.IsType<PromocionNormal>(estrategia);
        Assert.Equal(0m, estrategia.ObtenerPorcentajeDescuento());
    }

    [Fact]
    public void Seleccionar_CantidadNegativa_DebeLanzarExcepcion()
    {
        SelectorPromocion selector = new();

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            selector.Seleccionar(CategoriaCliente.VIP, -1));
    }
}
