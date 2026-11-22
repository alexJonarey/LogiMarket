namespace LogiMarket.Domain.Interfaces;

// ABSTRACT FACTORY:
// define una fábrica capaz de crear una familia
// de servicios relacionados con la entrega.
public interface IFabricaEntrega
{
    // Crea el servicio que calcula el costo.
    ICalculadorCostoEntrega CrearCalculadorCosto();

    // Crea el servicio que estima el tiempo.
    IEstimadorTiempoEntrega CrearEstimadorTiempo();
}