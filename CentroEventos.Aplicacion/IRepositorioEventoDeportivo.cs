using System;

namespace CentroEventos.Aplicacion;

public interface IRepositorioEventoDeportivo
{
    public void AgregarEvento(EventoDeportivo eventoDeportivo);
    public void BajarEvento(int id_evento);
    public void ModificarEvento(int id_evento);
    public List<EventoDeportivo> ListarEventos();

}
