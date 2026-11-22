namespace LogiMarket.Domain.Interfaces;

// PRODUCTO ABSTRACTO:
// define el contrato para estimar los días de entrega.
public interface IEstimadorTiempoEntrega
{
    int ObtenerDiasEstimados();
}