using LogiMarket.Domain.Entities;
using Xunit;

namespace LogiMarket.Tests;

public class MetricasSesionTests
{
    [Fact]
    public void Instancia_DebeDevolverSiempreElMismoObjeto()
    {
        // Arrange
        MetricasSesion metricasA = MetricasSesion.Instancia;
        MetricasSesion metricasB = MetricasSesion.Instancia;

        // Assert
        Assert.Same(metricasA, metricasB);
    }

    [Fact]
    public void RegistrarPedido_DebeActualizarElContadorCompartido()
    {
        // Arrange
        MetricasSesion metricasA = MetricasSesion.Instancia;
        MetricasSesion metricasB = MetricasSesion.Instancia;

        int pedidosIniciales = metricasA.PedidosCreados;

        // Act
        metricasA.RegistrarPedido();

        // Assert
        Assert.Equal(
            pedidosIniciales + 1,
            metricasB.PedidosCreados);
    }

    [Fact]
    public void RegistrarPago_DebeIncrementarContador()
    {
        // Arrange
        MetricasSesion metricas = MetricasSesion.Instancia;

        int pagosIniciales = metricas.PagosProcesados;

        // Act
        metricas.RegistrarPago();

        // Assert
        Assert.Equal(
            pagosIniciales + 1,
            metricas.PagosProcesados);
    }

    [Fact]
    public void RegistrarCancelacion_DebeIncrementarContador()
    {
        // Arrange
        MetricasSesion metricas = MetricasSesion.Instancia;

        int cancelacionesIniciales = metricas.PedidosCancelados;

        // Act
        metricas.RegistrarCancelacion();

        // Assert
        Assert.Equal(
            cancelacionesIniciales + 1,
            metricas.PedidosCancelados);
    }
}