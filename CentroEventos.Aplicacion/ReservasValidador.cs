using System;

namespace CentroEventos.Aplicacion;

public class ReservasValidador(IRepositorioReserva repoReserva, IRepositorioPersona Ipersona)
{
    public bool Validar(Reserva reserva, out string mensajeError){

        mensajeError = "";
        Console.WriteLine("Validando...");
        if (!Ipersona.existeId(reserva.Idpersona))
        {
            mensajeError = "El dni no existe.\n";
        }
        if (!repoReserva.ExisteResposable(reserva.Idpersona))
        {
            mensajeError = "La persona no reservo. \n";
        }
        if (repoReserva.ExisteResposable(reserva.Idpersona) && repoReserva.existeReservaRegistrada(reserva.Idpersona,reserva.IdEven_Dep))
        {
            mensajeError = "Ya existe reserva para este evento /n";
        }
        if (!repoReserva.ExisteCupo(reserva.Id))
        {
            mensajeError = "No hay cupo en este evento";
        }
        return (mensajeError == "");


    }

}
