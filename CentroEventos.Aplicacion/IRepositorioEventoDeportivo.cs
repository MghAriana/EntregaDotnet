using System;

namespace CentroEventos.Aplicacion;

public interface IRepositorioEventoDeportivo
{
    public void AgregarEvento(EventoDeportivo eventoDeportivo);
    public void BajarEvento(int id);
    public void ModificarEvento(int id);

}
