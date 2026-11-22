using LogiMarket.Domain.Entities;
using Xunit;

namespace LogiMarket.Tests;

public class ServicioEntregaTests
{
    [Fact]
    public void EntregaEstandar_DebeDevolverCostoCincoYDiasTres()
    {
        // Arrange: seleccionamos la fábrica estándar.
        FabricaEntregaEstandar fabrica = new();

        ServicioEntrega servicio = new(fabrica);

        // Act: obtenemos el costo y el tiempo.
        decimal costo = servicio.ObtenerCosto();
        int dias = servicio.ObtenerDiasEstimados();

        // Assert: comprobamos los valores esperados.
        Assert.Equal(5m, costo);
        Assert.Equal(3, dias);
    }

    [Fact]
    public void EntregaExpres_DebeDevolverCostoDoceYUnDia()
    {
        // Arrange: seleccionamos la fábrica exprés.
        FabricaEntregaExpres fabrica = new();

        ServicioEntrega servicio = new(fabrica);

        // Act: obtenemos el costo y el tiempo.
        decimal costo = servicio.ObtenerCosto();
        int dias = servicio.ObtenerDiasEstimados();

        // Assert: comprobamos los valores esperados.
        Assert.Equal(12m, costo);
        Assert.Equal(1, dias);
    }

    [Fact]
    public void Constructor_FabricaNula_DebeLanzarExcepcion()
    {
        // Act y Assert: no se permite utilizar una fábrica nula.
        Assert.Throws<ArgumentNullException>(
            () => new ServicioEntrega(null!));
    }
}