
namespace LogiMarket.Domain.Entities;

// SINGLETON:
// garantiza que exista una única instancia de métricas de sesión.
public sealed class MetricasSesion
{
    // Lazy garantiza la creación controlada y segura de la instancia.
    private static readonly Lazy<MetricasSesion> _instancia =
        new(() => new MetricasSesion());

    // Punto de acceso a la instancia única.
    public static MetricasSesion Instancia => _instancia.Value;

    // Constructor privado: impide crear objetos desde otras clases.
    private MetricasSesion()
    {
    }

    public int PedidosCreados { get; private set; }

    public int PagosProcesados { get; private set; }

    public int PedidosCancelados { get; private set; }

    // Registra un pedido creado.
    public void RegistrarPedido()
    {
        PedidosCreados++;
    }

    // Registra un pago procesado.
    public void RegistrarPago()
    {
        PagosProcesados++;
    }

    // Registra un pedido cancelado.
    public void RegistrarCancelacion()
    {
        PedidosCancelados++;
    }
}
