
namespace LogiMarket.Domain.Interfaces;

// ITERATOR: define cómo recorrer los componentes físicos
// sin exponer la estructura interna de la colección.
public interface IIteradorComponentes
{
    bool TieneSiguiente();

    IComponenteCatalogo Siguiente();
}
