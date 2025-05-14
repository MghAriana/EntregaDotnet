using System;

namespace CentroEventos.Aplicacion;

public interface IRepositorioReserva
{
    public List<EventoDeportivo> ListarEventosConCupo();
}
