using System;
namespace CentroEventos.Aplicacion;

/*  No puede modificarse un EventoDeportivo cuya FechaHoraInicio haya expirado 
    (es decir, no puede modificarse un evento pasado).
    Al modificar un EventoDeportivo, no puede establecerse la FechaHoraInicio con un valor anterior al actual 
    (es decir que sólo se permite si la fecha que va a registrarse es >= fecha actual).*/

public class EventoDeportivoModificacionUseCase(IRepositorioEventoDeportivo repoE, IRepositorioReserva repoR, EventoDeportivoValidador validador)
{
    public void Ejecutar(int id_evento)
    {
        if (id_evento == -1)
        {
            throw new Exception("El id ingresado debe ser un numero entero mayor que 0.");
        }
        repoE.ModificarEvento(id_evento, repoR, validador);
    }
}
