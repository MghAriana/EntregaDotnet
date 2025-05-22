using System;

namespace CentroEventos.Aplicacion;

public interface IRepositorioEventoDeportivo
{
    public void AgregarEvento(EventoDeportivo eventoDeportivo);
    public void BajarEvento(int id_evento);
    public void ModificarEvento(int id_evento, IRepositorioReserva repoR, EventoDeportivoValidador validador);
    public List<EventoDeportivo> ListarEventos();
    public bool esResponsableDeEvento(int id_persona);

}
