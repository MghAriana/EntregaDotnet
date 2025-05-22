using System;

namespace CentroEventos.Aplicacion;

public class ListarReservaUseCase(IRepositorioReserva repoR)
{
    public List<Reserva> Ejecutar()
    {
        return repoR.ListarReserva();       
    }
}
