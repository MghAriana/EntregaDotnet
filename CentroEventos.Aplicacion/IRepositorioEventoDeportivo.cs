using System;

namespace CentroEventos.Aplicacion;

public interface IRepositorioEventoDeportivo
{
    public void AgregarEvento(EventoDeportivo eventoDeportivo);
    public bool ExistePersona(int responsableId);
    public List<EventoDeportivo> ListarEventosConCupo();
}
