using System;
namespace CentroEventos.Aplicacion;

/*  No puede eliminarse un EventoDeportivo si existen Reservas asociadas al mismo.
    (independientemente del estado de las reservas).*/

public class EventoDeportivoBajaUseCase(IRepositorioEventoDeportivo repoE,IRepositorioReserva repoR) //inyección de dependencias
{
    public void Ejecutar(int id_evento)
    {   
        if(repoR.existenReservas(id_evento))
        {
            throw new Exception("No se puede eliminar el evento porque existen reservas asociadas.");
        }
        repoE.BajarEvento(id_evento);
    }
}
