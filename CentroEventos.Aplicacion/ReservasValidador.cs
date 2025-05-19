using System;

namespace CentroEventos.Aplicacion;

public class ReservasValidador(IRepositorioReserva repoReserva, IRepositorioPersona Ipersona, IRepositorioEventoDeportivo Ieventos)
{
    public bool Validar(Reserva unareserva, out string mensajeError){

        mensajeError = "";

        if (!Ipersona.existeId(unareserva.Idpersona))
        {
            mensajeError = "El dni no existe.\n";
        }
        if (!repoReserva.ExisteResposable(unareserva.Idpersona))
        {
            mensajeError = "La persona no reservo. \n";
        }
        if (repoReserva.ExisteResposable(unareserva.Idpersona) && repoReserva.existenReservas(unareserva.IdEven_Dep))
        {
            mensajeError = "Ya existe reserva para este evento /n";
        }
        if (!repoReserva.ExisteCupo(unareserva.Id))
        {
            mensajeError = "No hay cupo en este evento";
        }
        return (mensajeError == "");


    }

}
