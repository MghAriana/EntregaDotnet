using System;

namespace CentroEventos.Aplicacion;

public interface IRepositorioEventoDeportivo
{
    public void AgregarEvento(EventoDeportivo eventoDeportivo);
    public void BajarEvento(int id);
    public bool ExisteResponsable(int responsableId);
    public List<EventoDeportivo> ListarEventosConCupo();

}
