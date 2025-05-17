using System;

namespace CentroEventos.Aplicacion;

public class ReservasValidador(IRepositorioReserva repoReserva,IRepositorioPersona Ipersona, IRepositorioEventoDeportivo Ieventos)
{
    public bool Validar(Reserva unareserva, out string mensajeError){

        mensajeError = "";

        if (!Ipersona.existeId(unareserva._Persona_id))
        {
            mensajeError = "El dni no existe. \n"
        }
        if (!Ieventos.ExistePersona (unareserva._Persona_id))
        {
            mensajeError = "La persona no reservo. \n"
        }
        if (repoReserva.ExistePersona(unareserva._Persona_id) && _repoReserva.existeReservaEvento(unareserva._EventoDeportivoid))
        {
            mensajeError = "Ya existe reserva para este evento /n"
        }
        int cantActReservas = repoReserva.contarPorEvento(unareserva._EventoDeportivoid);
        if (cantActReservas >= Ieventos.ListarEventosConCupo(unareserva._EventoDeportivoid))
        {//aceder a evento con id traer el cupo max y comparar el repo reservas para saber la cantidad de reserva
            mensajeError = "No hay cupo en este evento";
        }

        return mensajeError;


    }

}
