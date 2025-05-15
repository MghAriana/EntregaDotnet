using System;

namespace CentroEventos.Aplicacion;

public interface IRepositorioReserva
{
    public List<EventoDeportivo> ListarEventosConCupo();
    public bool ExistenReservas(int idEvento);
}
