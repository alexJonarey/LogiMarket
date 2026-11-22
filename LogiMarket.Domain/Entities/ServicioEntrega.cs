using LogiMarket.Domain.Interfaces;

namespace LogiMarket.Domain.Entities;

// SERVICIO:
// utiliza Abstract Factory para obtener
// el costo y el tiempo de una modalidad de entrega.
public class ServicioEntrega
{
    private readonly ICalculadorCostoEntrega _calculadorCosto;
    private readonly IEstimadorTiempoEntrega _estimadorTiempo;

    public ServicioEntrega(IFabricaEntrega fabricaEntrega)
    {
        if (fabricaEntrega == null)
            throw new ArgumentNullException(nameof(fabricaEntrega));

        // La fábrica proporciona los dos productos relacionados.
        _calculadorCosto = fabricaEntrega.CrearCalculadorCosto();
        _estimadorTiempo = fabricaEntrega.CrearEstimadorTiempo();
    }

    // Obtiene el costo de la entrega seleccionada.
    public decimal ObtenerCosto()
    {
        return _calculadorCosto.CalcularCosto();
    }

    // Obtiene los días estimados de entrega.
    public int ObtenerDiasEstimados()
    {
        return _estimadorTiempo.ObtenerDiasEstimados();
    }
}